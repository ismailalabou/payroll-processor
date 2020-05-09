using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PayrollProcessor.Web.Api.Features.Notifications;
using PayrollProcessor.Web.Api.Infrastructure.Routing;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using PayrollProcessor.Web.Api.Configuration.Persistence;
using LanguageExt;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PayrollProcessor.Core.Domain.Infrastructure.Serialization;

namespace PayrollProcessor.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            JsonConvert.DefaultSettings = () => DefaultJsonSerializerSettings.JsonSerializerSettings;

            services.AddCors(options =>
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithOrigins(Configuration.GetValue<string>("CORS:client:domain"))
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()));

            services.AddControllers(options => options.UseGlobalRoutePrefix("api/v{version:apiVersion}"));

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddSignalR();

            services
                .AddCosmosClient(Configuration)
                .AddQueueClient(Configuration)
                .AddCQRSTypes();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PayrollProcessor", Version = "v1" });
                c.EnableAnnotations();
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var logger = loggerFactory.CreateLogger("TryOptionAsync Fault");

            TryConfig.ErrorLogger = ex => logger.LogError(ex, "Failure");

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHub<NotificationHub>("/hub/notifications");
            });

            app.UseCors();

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payroll Processor API V1"));
        }
    }
}
