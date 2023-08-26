using FeedbackService.Core.Interfaces.Repositories;
using FeedbackService.Core.Interfaces.Services;
using FeedbackService.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedbackService.Core.Services;
using FeedbackService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using FeedbackService.Api.Middlewares;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using HealthChecks.UI.Configuration;
using System.Text.Json.Serialization;

namespace FeedbackService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure Cors.
            services.ConfigureCors();

            // Configure Dependency Injection.
            services.ConfigureDependencyInjection(Configuration);

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            });

            // Set the JSON serializer options.
            services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            // Configure Swagger.
            services.ConfigureSwagger();

            // Configure AutoMapper.
            services.AddAutoMapper(typeof(Startup));

            services.AddRouting(options => options.LowercaseUrls = true);
            // Configure ConfigureHealthChecks.
            //services.ConfigureHealthChecks(Configuration);
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpCodeAndLogMiddleware();
                app.UseHsts();
            }

            app.ConfigureSwagger(provider);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // HealthCheck middleware
                //endpoints.MapHealthChecks("/api/health", new HealthCheckOptions()
                //{
                //    Predicate = _ => true,
                //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                //});
                //app.UseHealthChecksUI(delegate (Options options)
                //{
                //    options.UIPath = "/healthcheck-ui";
                //    options.AddCustomStylesheet("./HealthCheck/Custom.css");
                //});
            });

        }
    }
}
