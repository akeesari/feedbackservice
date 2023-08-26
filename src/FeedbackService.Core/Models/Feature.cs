using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductService.Core.Models
{
    public class Feature
    {
        // ignored
        //[JsonIgnore]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        //public IEnumerable<FeatureDetails> FeatureDetails { get; set; }
    }
}
