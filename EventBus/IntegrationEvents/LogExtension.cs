using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace PMIS.EventBus.IntegrationEvents
{
    public static class LogExtension
    {
        //https://stackoverflow.com/questions/66658664/why-serilog-postgresql-sink-is-not-working-through-configuration
        public static IServiceCollection RegisterLogging(this IServiceCollection services, HostBuilderContext context)
        {
            var section = context.Configuration.GetSection("Serilog");

            try
            {
                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(section)
                    .CreateLogger();

                services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());

                Log.Information("Data processor started.");
            }
            catch (Exception ex)
            {

            }

            return services;
        }
    }
}