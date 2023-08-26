using System.ComponentModel.DataAnnotations;

namespace ProductService.Core.Models
{
    public class FeatureDetails
    {
        public int Id { get; set; }
        [Required]
        public int FeatureId { get; set; }
        public string Description { get; set; }
        [Required]
        public int ElementType { get; set; }
    }
}
