using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using NpgsqlTypes;
using Serilog.Sinks.PostgreSQL;
using Serilog.Sinks.PostgreSQL.ColumnWriters;

namespace PMIS.ProjectGql.IntegrationEvents
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
