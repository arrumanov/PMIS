using Microsoft.Extensions.DependencyInjection;

namespace PMIS.DogovorGql.IntegrationEvents
{
    public static class BusinessServiceExtension
    {
        public static IServiceCollection RegisterBusinessServices(this IServiceCollection services)
        {
            services.AddTransient<IProjectService, ProjectService>();

            return services;
        }
    }
}