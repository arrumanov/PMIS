using Microsoft.Extensions.DependencyInjection;
using System;

namespace PMIS.DogovorGql.IntegrationEvents
{
    public static class CorsServiceExtension
    {
        public static IServiceCollection RegisterCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("DefaultPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            return services;
        }
    }
}
