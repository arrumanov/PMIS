using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PMIS.ProjectGql.Data;
using PMIS.ProjectGql.Projects;
using PMIS.ProjectGql.Types;

namespace PMIS.ProjectGql
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<ProjectPortfolioDbContext>(
                options => options.UseSqlite("Data Source=Projects.db"));

            services
                .AddGraphQLServer()
                //.AddQueryType<ProjectQueries>()
                //.AddMutationType<ProjectsMutations>();
                .AddQueryType(d => d.Name("Query"))
                .AddTypeExtension<ProjectQueries>()
                .AddTypeExtension<ProjectQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                .AddTypeExtension<ProjectsMutations>()
                .AddType<ProjectType>()
                .EnableRelaySupport()
                .AddFiltering()
                .AddSorting();
                //.AddDataLoader<ProjectByIdDataLoader>();
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
