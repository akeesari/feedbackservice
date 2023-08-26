using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace FeedbackService.Api.Helper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Infrastructure.Entities.Feedback, Core.Models.Feedback>().ReverseMap();
            CreateMap<ProductService.Infrastructure.Entities.Feature, ProductService.Core.Models.Feature>().ReverseMap();
            CreateMap<ProductService.Infrastructure.Entities.FeatureDetails, ProductService.Core.Models.FeatureDetails>().ReverseMap();
        }
    }
}
