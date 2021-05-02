using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MassTransit;
using Microsoft.Extensions.Configuration;
using PMIS.EventBus.IntegrationEvents;
using Serilog;

namespace PMIS.EventBus
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureLogging(loggingBuilder =>
                {
                    var logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build())
                        .CreateLogger();
                    loggingBuilder.AddSerilog(logger, dispose: true);
                })
                .ConfigureAppConfiguration((hostingContext, config) => {

                    config.AddConfiguration(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build());
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.RegisterConfigurationServices(hostContext);
                    services.RegisterBusinessServices();
                    services.RegisterRepositoryServices();
                    services.RegisterLogging(hostContext);
                    services.RegisterQueueServices(hostContext);
                    services.AddHostedService<Worker>();
                });
    }
}
