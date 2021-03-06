using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ProjectPortfolio.Infrastructure.Database.Command
{
    public static class ConnectionFactory
    {
        public static DbContextOptionsBuilder SetConnectionConfig(this DbContextOptionsBuilder options, IOptions<DatabaseConfiguration> configuration)
        {
            var config = configuration.Value;

            switch (config.WriteDatabaseProvider)
            {
                case DatabaseProvider.POSTGRES:
                    return options
                        .UseNpgsql(config.WriteDatabase);
                            //, opt => opt.MigrationsAssembly("ProjectPortfolio.Api"));
                case DatabaseProvider.MSSQL:
                    return options
                        .UseSqlServer(config.WriteDatabase,
                            opt => opt.MigrationsAssembly("ProjectPortfolio.Api"));
                default:
                    return options
                        .UseNpgsql("Host=localhost; Port=5432; Database=TestPPApiOne; User Id=postgres;Password=A2t=A2t=");
                    //return options
                    //    .UseInMemoryDatabase("graphdb");
            }
        }
    }
}