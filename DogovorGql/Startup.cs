using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PMIS.DogovorGql.Contracts;
using PMIS.DogovorGql.Data;
using PMIS.DogovorGql.IntegrationEvents;
using PMIS.DogovorGql.Types;
using Serilog;
using StackExchange.Redis;

namespace PMIS.DogovorGql
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

            services.AddPooledDbContextFactory<DogovorDbContext>(
                //��������� ��� ������� � IIS
                options => options.UseNpgsql(Configuration.GetConnectionString("IISConnection")));
            services.AddSingleton(ConnectionMultiplexer.Connect("localhost:7000"));
            //��������� ��� ������� � Docker
            //    options => options.UseNpgsql(Configuration.GetConnectionString("DockerConnection")));
            //services.AddSingleton(ConnectionMultiplexer.Connect("redis:6379"));


            services
                .AddGraphQLServer()
                .AddQueryType(d => d.Name("Query"))
                .AddTypeExtension<ContractQueries>()
                .AddTypeExtension<ContractQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                .AddTypeExtension<ContractMutations>()
                .AddType<ContractType>()
                .EnableRelaySupport()
                .AddFiltering()
                .AddSorting()
                // We initialize the schema on startup so it is published to the redis as soon as possible
                .InitializeOnStartup()
                // We configure the publish definition
                .PublishSchemaDefinition(c => c
                    // The name of the schema. This name should be unique
                    .SetName("dogovors")
                    .PublishToRedis(
                        // The configuration name under which the schema should be published
                        "PMIS",
                        // The connection multiplexer that should be used for publishing
                        sp => sp.GetRequiredService<ConnectionMultiplexer>()));

            services.RegisterCorsPolicy();
            services.RegisterConfigurationServices(Configuration);
            services.RegisterQueueServices(Configuration);
            services.RegisterBusinessServices();
            services.RegisterLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("DefaultPolicy");
            app.UseRouting();
            loggerFactory.AddSerilog();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}