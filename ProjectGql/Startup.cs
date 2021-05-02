using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PMIS.ProjectGql.Data;
using PMIS.ProjectGql.IntegrationEvents;
using PMIS.ProjectGql.Projects;
using PMIS.ProjectGql.Types;
using StackExchange.Redis;

namespace PMIS.ProjectGql
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
            services.AddPooledDbContextFactory<ProjectPortfolioDbContext>(
                //Настройки при запуске в IIS
                options => options.UseNpgsql(Configuration.GetConnectionString("IISConnection")));
            services.AddSingleton(ConnectionMultiplexer.Connect("localhost:7000"));
            //Настройки при запуске в Docker
            //    options => options.UseNpgsql(Configuration.GetConnectionString("DockerConnection")));
            //services.AddSingleton(ConnectionMultiplexer.Connect("redis:6379"));

            services
                .AddGraphQLServer()
                .AddQueryType(d => d.Name("Query"))
                .AddTypeExtension<ProjectQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                .AddTypeExtension<ProjectsMutations>()
                .AddType<ProjectType>()
                .EnableRelaySupport()
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
            //.AddDataLoader<ProjectByIdDataLoader>();

            services.AddHostedService<DWHWorker>();
            services.AddHostedService<ProjectIndexWorker>();
            services.RegisterConfigurationServices(Configuration);
            services.RegisterQueueServices(Configuration);
            services.RegisterRepositoryServices();
            services.RegisterLogging(Configuration);
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
