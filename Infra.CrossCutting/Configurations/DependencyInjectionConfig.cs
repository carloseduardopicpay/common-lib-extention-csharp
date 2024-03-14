using Microsoft.Extensions.DependencyInjection;

namespace Infra.CrossCutting.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            DependencyInjector.Register(services);
        }
    }
}
