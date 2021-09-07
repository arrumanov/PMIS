using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Workflow.Api.Domain;

namespace Workflow.Api.DataAccess
{
    public class WorkflowContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public WorkflowContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        class ProjectConfiguration : IEntityTypeConfiguration<Project>
        {
            public void Configure(EntityTypeBuilder<Project> modelBuilder)
            {
                modelBuilder.HasKey(s => s.Id);
                modelBuilder.Property(s => s.Status).HasConversion(new EnumToStringConverter<ProjectStatus>());
                modelBuilder.Property(s => s.ProcessInstanceId);
            }
        }

        class NotificationConfiguration : IEntityTypeConfiguration<Notification>
        {
            public void Configure(EntityTypeBuilder<Notification> modelBuilder)
            {
                modelBuilder.HasKey(s => s.Id);
                modelBuilder.Property(s => s.Text);
                modelBuilder.Property(s => s.TargetGroup);
                modelBuilder.Property(s => s.TargetUser);
                modelBuilder.Property(s => s.IsRead);
            }
        }
    }
}