using System;
using System.Linq;
using ProjectPortfolio.CrossCutting;
using ProjectPortfolio.Infrastructure.Database.Command.Model;
using Microsoft.EntityFrameworkCore;

namespace ProjectPortfolio.Infrastructure.Database.Command
{
    public class ProjectPortfolioContext : DbContext
    {
        public ProjectPortfolioContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var statuses = Enum.GetValues(typeof(TaskStatusEnum)).OfType<TaskStatusEnum>().Select(i => new Status() { Id = (int)i, Description = i.ToString() });

            modelBuilder.Entity<Status>().HasData(statuses);

            modelBuilder.Entity<User>(e =>
            {
                e.HasIndex(i => i.Email).IsUnique();
                e.Property(i => i.Email).HasMaxLength(100).IsRequired();
                e.Property(i => i.Name).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<Project>(e =>
            {
                e.HasMany(i => i.Tasks).WithOne(i => i.Project);
                e.Property(p => p.DepartmentIds)
                    .HasConversion(
                        srts => string.Join(", ", srts != null ? srts.ToArray() : new Guid[] { }),
                        srts => srts.Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList()
                            .ConvertAll(Guid.Parse));
                e.Property(p => p.ContragentIds)
                    .HasConversion(
                        srts => string.Join(", ", srts != null ? srts.ToArray() : new Guid[] { }),
                        srts => srts.Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList()
                            .ConvertAll(Guid.Parse));
                e.Property(p => p.ProductIds)
                    .HasConversion(
                        srts => string.Join(", ", srts != null ? srts.ToArray() : new Guid[] { }),
                        srts => srts.Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList()
                            .ConvertAll(Guid.Parse));
            });

            modelBuilder.Entity<UserProject>(e =>
            {
                e.Ignore(i => i.Id);
                e.HasKey(i => new { ProjectId = i.ProjectId, i.UserId });
            });
        }
    }
}