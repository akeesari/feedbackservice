using FeedbackService.Core.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackService.Api.SwaggerExamples.Requests
{
    public class FeedbackRequestExample : IExamplesProvider<Feedback>
    {
        public Feedback GetExamples()
        {
            return new Feedback
            {
                Subject = "Authentication",
                Message = "really nice to have this course in your list",
                Rating = 10,
                CreatedBy = "anji keesari",
                CreatedDate = DateTime.Now
            };
        }
    }
}
