using Microsoft.Extensions.DependencyInjection;

namespace PMIS.EventBus.IntegrationEvents
{
    public static class RepositoryServiceExtension
    {
        public static IServiceCollection RegisterRepositoryServices(this IServiceCollection services)
        {
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IDataContext, DataContext>();

            return services;
        }
    }
}