using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Dogovor.Infrastructure.Database.Command
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
                            //, opt => opt.MigrationsAssembly("Dogovor.Api"));
                case DatabaseProvider.MSSQL:
                    return options
                        .UseSqlServer(config.WriteDatabase,
                            opt => opt.MigrationsAssembly("Dogovor.Api"));
                default:
                    return options
                        .UseInMemoryDatabase("graphdb");
            }
        }
    }
}