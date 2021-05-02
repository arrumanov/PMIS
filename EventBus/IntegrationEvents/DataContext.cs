using Microsoft.EntityFrameworkCore;
using PMIS.Contracts;

namespace PMIS.EventBus.IntegrationEvents
{
    public class DataContext : DbContext, IDataContext
    {
        private readonly ConnectionStrings _connectionStrings;

        public DataContext(ConnectionStrings connectionStrings)
        {
            _connectionStrings = connectionStrings;
        }

        public void EnsureDbCreated()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(_connectionStrings.Main);
            optionsBuilder.UseNpgsql(_connectionStrings.Main);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>().HasKey(x => x.Id);
        }

        public DbSet<Project> Projects { get; set; }
    }
}