using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using StackExchange.Redis;

namespace ApiGateways.Gql
{
    public class Startup
    {
        public const string Projects = "projects";
        public const string Dogovors = "dogovors";
        public const string Permissions = "permissions";
        public const string Workflows = "workflows";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("DefaultPolicy", builder =>
                {
                    builder.AllowAnyHeader()
                        .WithMethods("GET", "POST")
                        .WithOrigins("http://localhost:1234");
                });
            });

            //????????? ??? ??????? ? IIS
            services.AddHttpClient(Projects, c => c.BaseAddress = new Uri("https://localhost:44342/graphql"));
            services.AddHttpClient(Dogovors, c => c.BaseAddress = new Uri("https://localhost:44343/graphql"));
            services.AddHttpClient(Permissions, c => c.BaseAddress = new Uri("https://localhost:44359/graphql"));
            services.AddHttpClient(Workflows, c => c.BaseAddress = new Uri("https://localhost:44314/graphql"));

            //https://alikzlda.medium.com/a-simple-pub-sub-scenario-with-masstransit-6-2-rabbitmq-net-core-3-1-elasticsearch-mssql-5a65c993b2fd
            //docker run -p 9200:9200 -p 9300:9300 --network elasticsearchnetwork -e "discovery.type=single-node" --name elasticsearch elasticsearch:7.6.2
            //docker run --network elasticsearchnetwork --name kibana -p 5601:5601 kibana:7.6.2
            //docker container run -d --name some-rabbit -p 4369:4369 -p 5671:5671 -p 5672:5672 -p 25672:25672 -p 15671:15671 -p 8080:15672 rabbitmq:3-management
            //https://www.michalbialecki.com/2020/04/23/set-up-a-sql-server-in-a-docker-container/ - ?????? ? ?????????? MS SQl Server
            //docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=myPass123" -p 1433:1433 --name primeHotelDb -d mcr.microsoft.com/mssql/server:2017-latest
            //https://github.com/ChilliCream/hotchocolate-examples/tree/master/misc/Stitching/federated-with-hot-reload
            //docker run --name redis-stitching -p 7000:6379 -d redis
            services.AddSingleton(ConnectionMultiplexer.Connect("localhost:7000"));

            //????????? ??? ??????? ? Docker
            //services.AddHttpClient(Projects, c => c.BaseAddress = new Uri("http://projectgql/graphql"));
            //services.AddHttpClient(Dogovors, c => c.BaseAddress = new Uri("http://dogovorgql/graphql"));
            //services.AddSingleton(ConnectionMultiplexer.Connect("redis:6379"));

            services
                .AddGraphQLServer()
                //.AddQueryType(d => d.Name("Query"))
                //.AddMutationType(d => d.Name("Mutation"))
                .AddRemoteSchemasFromRedis("PMIS", sp => sp.GetRequiredService<ConnectionMultiplexer>());
            //.AddTypeExtensionsFromFile("./Stitching.graphql");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // ?????????? CORS
            app.UseCors("DefaultPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
