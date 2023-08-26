using ProductService.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductService.Core.Interfaces.Repositories
{
    public interface IFeatureRepository
    {
        Task<IEnumerable<Feature>> GetAll();
        Task<IEnumerable<FeatureDetails>> GetFeatureDetailsByFeatureId(int featureId);     
        Task<Feature> GetById(int id);
        //Task<Feature> GetByProductId(int productId);
    }
}
