using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Permission.Infrastructure.Extensions
{
    public static class LogExtension
    {
        //https://stackoverflow.com/questions/66658664/why-serilog-postgresql-sink-is-not-working-through-configuration
        public static IServiceCollection RegisterLogging(this IServiceCollection services, IConfiguration configuration)
        {
            //var section = context.Configuration.GetSection("Serilog");

            var serilogSection = configuration.GetSection("Serilog");

            try
            {
                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(serilogSection)
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