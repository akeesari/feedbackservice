using AutoMapper;
using FeedbackService.Core.Interfaces.Repositories;
using FeedbackService.Core.Models;
using FeedbackService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using ProductService.Core.Interfaces.Repositories;
using ProductService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Repositories
{
    public class FeatureRepository : IFeatureRepository
    {
        private readonly FeedbackDbContext _dbcontext;
        private readonly IMapper _mapper;
        public FeatureRepository(FeedbackDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<Feature>> GetAll()
        {
            var features = await _dbcontext.Feature.ToListAsync().ConfigureAwait(false);
            if (features != null)
            {
                return _mapper.Map<IEnumerable<Feature>>(features);
            }
            return null;
        }

        public async Task<Feature> GetById(int id)
        {
            var feature = await _dbcontext.Feature.SingleAsync(f => f.Id == id).ConfigureAwait(false);
            var result = new Feature
            {
                Id = feature.Id,
                ProductId = feature.ProductId,
                Name = feature.Name,
                Description = feature.Description,
                CreatedBy = feature.CreatedBy,
                CreatedDate = feature.CreatedDate
            };
            return result;
        }
        //public async Task<Feature> GetByProductId(int productId)
        //{
        //    var feature = await _dbcontext.Feature.SingleAsync(f => f.ProductId == productId).ConfigureAwait(false);
        //    var result = new Feature
        //    {
        //        Id = feature.Id,
        //        ProductId = feature.ProductId,
        //        Name = feature.Name,
        //        Description = feature.Description,
        //        CreatedBy = feature.CreatedBy,
        //        CreatedDate = feature.CreatedDate
        //    };
        //    return result;
        //}

        public async Task<IEnumerable<FeatureDetails>> GetFeatureDetailsByFeatureId(int featureId)
        {
            var featureDetails = await _dbcontext.FeatureDetails.Where(f => f.FeatureId == featureId).ToListAsync().ConfigureAwait(false);
            if (featureDetails != null)
            {
                return _mapper.Map<IEnumerable<FeatureDetails>>(featureDetails);
            }
            return null;
        }
    }
}
