using ProductService.Core.Interfaces.Repositories;
using ProductService.Core.Interfaces.Services;
using ProductService.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductService.Core.Services
{
    public class FeatureService : IFeatureService
    {
        public readonly IFeatureRepository _featureRepository;
        private readonly ILogger<FeatureService> _logger;
        public FeatureService(IFeatureRepository featureRepository, ILogger<FeatureService> logger)
        {
            _featureRepository = featureRepository ?? throw new ArgumentNullException(nameof(featureRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<IEnumerable<Feature>> GetAll()
        {
            try
            {
                return await _featureRepository.GetAll();
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error while trying to call GetAll in service class, Error Message = {exception}.");
                throw;
            }
        }
        public async Task<Feature> GetById(int id)
        {
            try
            {
                return await _featureRepository.GetById(id);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error while trying to call GetFeatureById in service class, Error Message = {exception}.");
                throw;
            }
        }
        //public async Task<Feature> GetByProductId(int productId)
        //{
        //    try
        //    {
        //        return await _featureRepository.GetByProductId(productId);
        //    }
        //    catch (Exception exception)
        //    {
        //        _logger.LogError($"Error while trying to call GetByProductId in service class, Error Message = {exception}.");
        //        throw;
        //    }
        //}

        public async Task<IEnumerable<FeatureDetails>> GetFeatureDetailsByFeatureId(int featureId)
        {
            try
            {
                return await _featureRepository.GetFeatureDetailsByFeatureId(featureId);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error while trying to call GetFeatureDetailsByFeatureId in service class, Error Message = {exception}.");
                throw;
            }
        }
    }
}
