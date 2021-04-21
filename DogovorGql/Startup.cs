using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PMIS.DogovorGql.Contracts;
using PMIS.DogovorGql.Data;
using PMIS.DogovorGql.Types;

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
                options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddGraphQLServer()
                //.AddQueryType<ContractQueries>()
                //.AddMutationType<ContractsMutations>();
                .AddQueryType(d => d.Name("Query"))
                .AddTypeExtension<ContractQueries>()
                .AddTypeExtension<ContractQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                .AddTypeExtension<ContractMutations>()
                .AddType<ContractType>()
                .EnableRelaySupport()
                .AddFiltering()
                .AddSorting();
            //.AddDataLoader<ContractByIdDataLoader>();
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
