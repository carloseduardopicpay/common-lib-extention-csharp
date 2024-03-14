using HealthChecks.UI.Client;
using Infra.CrossCutting.Configurations;
using Infra.CrossCutting.HealthCheck;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks()
            .AddCheck<HealthCheck>("Aplicacao");
            services.AddControllers();

            services.AddMvc();
            services.AddDependencyInjectionConfiguration();
            services.AddSwaggerConfiguration();
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerSetup();

            app.UseCors(options => options.AllowAnyOrigin());
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");

                app.UseHealthChecks("/healthcheck", new HealthCheckOptions()
                {
                    ResponseWriter = (httpContext, result) =>
                    {
                        httpContext.Response.ContentType = "application/json";
                        var json = new JObject(
                            new JProperty("StatusGeral", result.Status.ToString()),
                            new JProperty("results", new JObject(result.Entries.Select(pair =>
                                new JProperty(pair.Key, new JObject(
                                    new JProperty("status", pair.Value.Status.ToString()),
                                    new JProperty("description", pair.Value.Description),
                                    new JProperty("data", new JObject(pair.Value.Data.Select(
                                        p => new JProperty(p.Key, p.Value))))))))));
                        return httpContext.Response.WriteAsync(json.ToString(Formatting.Indented));
                    }
                });
            });


            app.UseHealthChecks("/health-api", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI(opt =>
            {
                opt.UIPath = "/health-dashboard";
            });
        }
    }
}
