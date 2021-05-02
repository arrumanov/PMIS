using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PMIS.EventBus.IntegrationEvents
{
    public static class ConfigurationServiceExtension
    {
        public static IServiceCollection RegisterConfigurationServices(this IServiceCollection services, HostBuilderContext context)
        {
            var connectionStrings = new ConnectionStrings();
            var queueSettings = new QueueSettings();

            context.Configuration.GetSection("ConnectionStrings").Bind(connectionStrings);
            context.Configuration.GetSection("QueueSettings").Bind(queueSettings);

            services.AddSingleton(connectionStrings);
            services.AddSingleton(queueSettings);

            return services;
        }
    }
}