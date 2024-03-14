using Application.Services;
using Domain.Interfaces.IRepository;
using Domain.Interfaces.IServices;
using Infra.Data.Contexts;
using Infra.Data.Repositories.ExportarInfobanc;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.CrossCutting.Configurations
{
    public static class DependencyInjector
    {
        public static void Register(this IServiceCollection services)
        {
            services.AddScoped<DbContextMemory>();
            services.AddScoped<IInfobankrepository, InfobancRepository>();
            services.AddScoped<IInfobancService, InfobancService>();
            services.AddScoped<ILogService, LoggerService>();

            services.AddScoped<IExportarPessoasInfobancRepository, ExportarPessoasInfobancRepository>();
            services.AddScoped<IExportarEnderecoInfobancRepository, ExportarEnderecoInfobancRepository>();
        }
    }
}
