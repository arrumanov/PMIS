using Dogovor.Infrastructure.Database.Command.Model;
using Dogovor.Infrastructure.Database.Query.Model.Client;
using Microsoft.EntityFrameworkCore;

namespace Dogovor.Infrastructure.Database.Command
{
    public class GraphContext : DbContext
    {
        public GraphContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Contract> Contragent { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contract>().HasKey(p => new { p.Id });
        }
    }
}