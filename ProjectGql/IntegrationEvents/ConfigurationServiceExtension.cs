using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PMIS.ProjectGql.IntegrationEvents
{
    public static class ConfigurationServiceExtension
    {
        public static IServiceCollection RegisterConfigurationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //var connectionStrings = new ConnectionStrings();
            //var elasticsearchSettings = new ElasticSearchSettings();
            //var queueSettings = new QueueSettings();

            //context.Configuration.GetSection("ConnectionStrings").Bind(connectionStrings);
            //context.Configuration.GetSection("Elastic").Bind(elasticsearchSettings);
            //context.Configuration.GetSection("QueueSettings").Bind(queueSettings);

            //services.AddSingleton(elasticsearchSettings);
            //services.AddSingleton(queueSettings);
            //services.AddSingleton(connectionStrings);


            var connectionStringsSection = configuration.GetSection("ConnectionStrings");
            var connectionStrings = connectionStringsSection.Get<ConnectionStrings>();
            services.Configure<ConnectionStrings>(connectionStringsSection);
            services.AddSingleton(connectionStrings);


            var elasticsearchSettingsSection = configuration.GetSection("Elastic");
            var elasticsearchSettings = elasticsearchSettingsSection.Get<ElasticSearchSettings>();
            services.Configure<Appsettings>(elasticsearchSettingsSection);
            services.AddSingleton(elasticsearchSettings);

            var queueSettingsSection = configuration.GetSection("QueueSettings");
            var queueSettings = queueSettingsSection.Get<QueueSettings>();
            services.Configure<Appsettings>(queueSettingsSection);
            services.AddSingleton(queueSettings);

            return services;
        }
    }
}