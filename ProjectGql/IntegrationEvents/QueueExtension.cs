using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace PMIS.ProjectGql.IntegrationEvents
{
    public static class QueueExtension
    {
        public static IServiceCollection RegisterQueueServices(this IServiceCollection services, IConfiguration configuration)
        {
            //var queueSettings = new QueueSettings();
            //context.Configuration.GetSection("QueueSettings").Bind(queueSettings);

            var queueSettingsSection = configuration.GetSection("QueueSettings");
            var queueSettings = queueSettingsSection.Get<QueueSettings>();

            services.AddMassTransit(c =>
            {
                c.AddConsumer<ProjectIndexConsumer>();
                c.AddConsumer<DWHConsumer>();
            });

            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(queueSettings.HostName, queueSettings.VirtualHost, h => {
                    h.Username(queueSettings.UserName);
                    h.Password(queueSettings.Password);
                });
                //TODO здесь, почему-то, возникает ошибка - не видит такого метода
                //cfg.SetLoggerFactory(provider.GetService<ILoggerFactory>());

            }));
            return services;
        }
    }
}