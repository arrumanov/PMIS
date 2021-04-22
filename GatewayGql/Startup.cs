using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace GatewayGql
{
    public class Startup
    {
        public const string Projects = "projects";
        public const string Dogovors = "dogovors";

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient(Projects, c => c.BaseAddress = new Uri("http://localhost:23582/graphql"));
            services.AddHttpClient(Dogovors, c => c.BaseAddress = new Uri("http://localhost:6092/graphql"));
            //services.AddSingleton(ConnectionMultiplexer.Connect("redis:6379"));
            services.AddSingleton(ConnectionMultiplexer.Connect("localhost:7000"));

            //services
            //    .AddGraphQLServer()
            //    // add the remote schemas
            //    .AddRemoteSchema(Projects)
            //    .AddRemoteSchema(Dogovors);

            services
                .AddGraphQLServer()
                //.AddQueryType(d => d.Name("Query"))
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
