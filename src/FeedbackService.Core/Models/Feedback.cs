using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FeedbackService.Core.Models
{
    public class Feedback
    {
        // ignored
        //[JsonIgnore]
        public int Id { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required] 
        public string Message { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required] 
        public string CreatedBy { get; set; }
        [Required] 
        public DateTime CreatedDate { get; set; }
    }
}
