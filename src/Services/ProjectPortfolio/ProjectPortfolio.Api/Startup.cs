using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectPortfolio.CrossCutting.Ioc;
using ProjectPortfolio.Infrastructure.Database;
using ProjectPortfolio.Infrastructure.Database.Command;
using ProjectPortfolio.Infrastructure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjectPortfolio.Application.Graph.Project.Mutation;
using ProjectPortfolio.Application.Graph.Project.Query;
using ProjectPortfolio.Infrastructure.Extensions;
using StackExchange.Redis;

namespace ProjectPortfolio.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<BusConfiguration>(Configuration.GetSection("ServiceBus"));
            services.Configure<DatabaseConfiguration>(Configuration.GetSection("ConnectionStrings"));

            services.AddSingleton(ConnectionMultiplexer.Connect("localhost:7000"));

            services.AddControllers();

            services.ResolveServiceBus();
            services.ResolveCommandDatabase();
            services.ResolveQueryDatabase();
            services.ResolveGraphDependencies();
            services.ResolveRequestHandlers();
            services.ResolveAuxiliaries();

            //-------- Hot Chocolate -----------//

            services
                .AddGraphQLServer()
                //.AddQueryType<ProjectQuery>()
                //.AddMutationType<ProjectMutation>()
                .AddQueryType(d => d.Name("Query"))
                .AddTypeExtension<ProjectQuery>()
                .AddMutationType(d => d.Name("Mutation"))
                .AddTypeExtension<ProjectMutation>()
                //.EnableRelaySupport()
                .AddFiltering()
                .AddSorting()
                // We initialize the schema on startup so it is published to the redis as soon as possible
                .InitializeOnStartup()
                // We configure the publish definition
                .PublishSchemaDefinition(c => c
                    // The name of the schema. This name should be unique
                    .SetName("projects")
                    .PublishToRedis(
                        // The configuration name under which the schema should be published
                        "PMIS",
                        // The connection multiplexer that should be used for publishing
                        sp => sp.GetRequiredService<ConnectionMultiplexer>()));

            //-------- Hot Chocolate -----------//


            //-------- Serilog -----------//

            services.RegisterLogging(Configuration);

            //-------- Serilog -----------//
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //-------- Hot Chocolate -----------//

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });

            //-------- Hot Chocolate -----------//

            app.UseHttpsRedirection();
            UpdateDatabase(app);
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<ProjectPortfolioContext>())
                {
                    context.Database.EnsureCreated();
                    if (!context.Database.IsInMemory()) context.Database.Migrate();
                }
            }
        }
    }
}
