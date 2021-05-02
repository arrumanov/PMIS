using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PMIS.EventBus.IntegrationEvents
{
    public static class QueueExtension
    {
        public static IServiceCollection RegisterQueueServices(this IServiceCollection services, HostBuilderContext context)
        {
            var queueSettings = new QueueSettings();
            context.Configuration.GetSection("QueueSettings").Bind(queueSettings);

            services.AddMassTransit(c =>
            {
                c.AddConsumer<ProjectChangedConsumer>();
            });

            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(queueSettings.HostName, queueSettings.VirtualHost, h => {
                    h.Username(queueSettings.UserName);
                    h.Password(queueSettings.Password);
                });
                //cfg.SetLoggerFactory((ILoggerFactory)provider.GetService(typeof(ILoggerFactory)));

            }));

            return services;
        }
    }
}