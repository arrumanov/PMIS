using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using StackExchange.Redis;

namespace GatewayGql
{
    public class Startup
    {
        public const string Projects = "projects";
        public const string Dogovors = "dogovors";
        
        public void ConfigureServices(IServiceCollection services)
        {
            //Настройки при запуске в IIS
            //services.AddHttpClient(Projects, c => c.BaseAddress = new Uri("http://localhost:23582/graphql"));
            //services.AddHttpClient(Dogovors, c => c.BaseAddress = new Uri("http://localhost:6092/graphql"));
            //https://github.com/ChilliCream/hotchocolate-examples/tree/master/misc/Stitching/federated-with-hot-reload
            //docker run --name redis-stitching -p 7000:6379 -d redis
            //services.AddSingleton(ConnectionMultiplexer.Connect("localhost:7000"));

            //Настройки при запуске в Docker
            services.AddHttpClient(Projects, c => c.BaseAddress = new Uri("http://projectgql/graphql"));
            services.AddHttpClient(Dogovors, c => c.BaseAddress = new Uri("http://dogovorgql/graphql"));
            services.AddSingleton(ConnectionMultiplexer.Connect("redis:6379"));

            services
                .AddGraphQLServer()
                .AddRemoteSchemasFromRedis("PMIS", sp => sp.GetRequiredService<ConnectionMultiplexer>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
