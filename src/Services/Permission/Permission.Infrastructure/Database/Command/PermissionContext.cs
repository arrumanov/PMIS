using Permission.Infrastructure.Database.Command.Model;
using Microsoft.EntityFrameworkCore;

namespace Permission.Infrastructure.Database.Command
{
    public class PermissionContext : DbContext
    {
        public PermissionContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(p => new { p.Id });
        }
    }
}