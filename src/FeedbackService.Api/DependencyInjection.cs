using FeedbackService.Core.Interfaces.Repositories;
using FeedbackService.Core.Interfaces.Services;
using FeedbackService.Core.Services;
using FeedbackService.Infrastructure.Context;
using FeedbackService.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Core.Interfaces.Repositories;
using ProductService.Core.Interfaces.Services;
using ProductService.Core.Services;
using ProductService.Infrastructure.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackService.Api
{
    public static class DependencyInjection
    {
		public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
		{
			if (services == null)
			{
				throw new ArgumentNullException(nameof(services));
			}

			if (configuration == null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

            //services.AddDbContext<FeedbackDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
            var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();

            string feedbackDbConnectionString = string.Empty;
            if ((bool)(appSettings?.ByPassKeyVault)) // use for localhost
            {
                feedbackDbConnectionString = configuration.GetConnectionString("Feedback");
            }
            else // used in environment
            {
                feedbackDbConnectionString = GetSecret.FeedbackDbConnectionString().Result;
            }
            // FeedbackDbContext database
            services.AddDbContext<FeedbackDbContext>(
            optionsAction: options => options.UseSqlServer(feedbackDbConnectionString),
            contextLifetime: ServiceLifetime.Transient,
            optionsLifetime: ServiceLifetime.Transient);


            services.AddScoped<IFeedbackService, FeedbacksService>();
			services.AddScoped<IFeedbackRepository, FeedbackRepository>();

            services.AddScoped<IFeatureService, FeatureService>();
            services.AddScoped<IFeatureRepository, FeatureRepository>();

            services.AddHttpClient();

            return services;
		}
	}
}
