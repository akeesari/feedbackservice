using FeedbackService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using ProductService.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackService.Infrastructure.Context
{
    public class FeedbackDbContext: DbContext
    {
        public FeedbackDbContext()
        {
        }
        public FeedbackDbContext(DbContextOptions<FeedbackDbContext> option): base (option)
        {
            //SeedData();
        }
        
        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<Feature> Feature { get; set; }
        public virtual DbSet<FeatureDetails> FeatureDetails { get; set; }
        //private void SeedData()
        //{
        //    var feedbacks = new List<Feedback>()
        //        {
        //            new Feedback() { Id =1, Subject = ".NET Core 5 Web API", Message = "Excellent training, would like to see more of such trainings", Rating = 10, CreatedBy = "Anji Keesari", CreatedDate = DateTime.Now },
        //            new Feedback() { Id =2,Subject = "Microservices", Message = "Needs improvements, looking for more videos", Rating = 7 , CreatedBy = "Anji Keesari", CreatedDate = DateTime.Now},
        //            new Feedback() { Id =3,Subject = "Azure DevOps", Message = "Please provide more along with source code", Rating = 5, CreatedBy = "Anji Keesari", CreatedDate = DateTime.Now },
        //            new Feedback() { Id =4,Subject = "Azure Cloud", Message = "Excellent training, continue providing more trainings", Rating = 10, CreatedBy = "Anji Keesari", CreatedDate = DateTime.Now }
        //        };

        //    Feedbacks.AddRange(feedbacks);
        //    SaveChanges();
        //}
    }
}
