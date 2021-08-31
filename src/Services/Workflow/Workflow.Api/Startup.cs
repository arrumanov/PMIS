using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Converters;
using StackExchange.Redis;
using Workflow.Api.Bpmn;
using Workflow.Api.DataAccess;
using Workflow.Api.Graph.Project.Mutation;
using Workflow.Api.Graph.Project.Query;
using Workflow.Api.Init;

namespace Workflow.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();

            services.AddSingleton(ConnectionMultiplexer.Connect("localhost:7000"));

            services
                .AddMvc()
                .AddNewtonsoftJson(opt => opt.SerializerSettings.Converters.Add(new StringEnumConverter()))
                //.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PlaceOrderDtoValidator>())
                ;

            services.AddMediatR(typeof(Startup));
            services.AddDataAccess(Configuration.GetConnectionString("HeroesDb"));
            services.AddDbInitializer();
            services.AddCamunda(appSettings.CamundaRestApiUri);

            services.AddSingleton<ProjectWorkflowQuery>();
            services.AddSingleton<ProjectWorkflowMutation>();

            //-------- Hot Chocolate -----------//

            services
                .AddGraphQLServer()
                .AddQueryType(d => d.Name("Query"))
                .AddTypeExtension<ProjectWorkflowQuery>()
                .AddMutationType(d => d.Name("Mutation"))
                .AddTypeExtension<ProjectWorkflowMutation>()
                //.EnableRelaySupport()
                //.AddFiltering()
                //.AddSorting()
                // We initialize the schema on startup so it is published to the redis as soon as possible
                .InitializeOnStartup()
                // We configure the publish definition
                .PublishSchemaDefinition(c => c
                    // The name of the schema. This name should be unique
                    .SetName("workflows")
                    .PublishToRedis(
                        // The configuration name under which the schema should be published
                        "PMIS",
                        // The connection multiplexer that should be used for publishing
                        sp => sp.GetRequiredService<ConnectionMultiplexer>()));

            //-------- Hot Chocolate -----------//
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
                using (var context = serviceScope.ServiceProvider.GetRequiredService<WorkflowContext>())
                {
                    context.Database.EnsureCreated();
                }
            }
        }
    }
}
