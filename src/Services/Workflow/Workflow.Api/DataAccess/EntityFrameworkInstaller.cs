using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Workflow.Api.DataAccess
{
    public static class EntityFrameworkInstaller
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, string cnnString)
        {
            services.AddDbContext<WorkflowContext>(options =>
            {
                options.UseNpgsql(cnnString);
            });

            return services;
        }
    }
}