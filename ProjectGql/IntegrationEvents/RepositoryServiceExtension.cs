using Microsoft.Extensions.DependencyInjection;

namespace PMIS.ProjectGql.IntegrationEvents
{
    public static class RepositoryServiceExtension
    {
        public static IServiceCollection RegisterRepositoryServices(this IServiceCollection services)
        {
            services.AddTransient<IProjectIndexService, ProjectIndexService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IDataContext, DataContext>();
            services.AddTransient<IProjectRepository, ProjectRepository>();

            return services;
        }
    }
}