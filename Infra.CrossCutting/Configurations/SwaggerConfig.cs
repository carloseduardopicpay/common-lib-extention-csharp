using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Linq;

namespace Infra.CrossCutting.Configurations
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ms-ppb-acad-naturalperson", Version = "v1" });

                // Adicione suporte a versionamento do Swagger
                c.DocInclusionPredicate((version, apiDescription) =>
                {
                    if (!version.Equals("v1"))
                        return false;

                    var values = apiDescription.RelativePath
                        .Split('/')
                        .Select(v => v.Replace("v{version}", version));

                    apiDescription.RelativePath = string.Join('/', values);
                    return true;
                });
            });
        }

        public static void UseSwaggerSetup(this IApplicationBuilder app)
        {
            app.UseSwagger();


            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Natural Person - Pessoa Fisíca");
                c.RoutePrefix = "swagger";
            });
        }
    }
}
