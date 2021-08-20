using Dogovor.Infrastructure.Database.Command.Model;
using Microsoft.EntityFrameworkCore;

namespace Dogovor.Infrastructure.Database.Command
{
    public class GraphContext : DbContext
    {
        public GraphContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Contract> Contract { get; set; }
        public DbSet<Contragent> Contragent { get; set; }
        public DbSet<Product> Subsystem { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<User> User { get; set; }

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