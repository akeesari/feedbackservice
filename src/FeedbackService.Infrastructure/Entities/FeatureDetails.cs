using System.ComponentModel.DataAnnotations;

namespace ProductService.Infrastructure.Entities
{
    public class FeatureDetails
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int FeatureId { get; set; }
        public string Description { get; set; }
        [Required]
        public int ElementType { get; set; }
    }
}
