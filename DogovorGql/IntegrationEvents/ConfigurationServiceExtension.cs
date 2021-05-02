using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PMIS.DogovorGql.IntegrationEvents
{
    public static class ConfigurationServiceExtension
    {
        public static IServiceCollection RegisterConfigurationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<Appsettings>();
            services.Configure<Appsettings>(appSettingsSection);
            services.AddSingleton(appSettings);

            return services;
        }
    }
}