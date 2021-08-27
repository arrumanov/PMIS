using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectPortfolio.CrossCutting;
using ProjectPortfolio.Infrastructure.Database.Command.Model;

namespace ProjectPortfolio.Infrastructure.Database.Command.EntityTypeConfiguration
{
    public class StatusEntityTypeConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            var statuses = Enum.GetValues(typeof(TaskStatusEnum)).OfType<TaskStatusEnum>().Select(i => new Status() { Id = (int)i, Description = i.ToString() });
            builder.HasData(statuses);
        }
    }
    
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(i => i.Email).IsUnique();
            builder.Property(i => i.Email).HasMaxLength(100).IsRequired();
            builder.Property(i => i.Name).HasMaxLength(100).IsRequired();
        }
    }

    public class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(i => i.Name).HasMaxLength(120).IsRequired();
        }
    }

    public class UserProjectEntityTypeConfiguration : IEntityTypeConfiguration<UserProject>
    {
        public void Configure(EntityTypeBuilder<UserProject> builder)
        {
            builder.Ignore(i => i.Id);
            builder.HasKey(i => new { ProjectId = i.ProjectId, i.UserId });
        }
    }

    public class ProjectDepartmentEntityTypeConfiguration : IEntityTypeConfiguration<ProjectDepartment>
    {
        public void Configure(EntityTypeBuilder<ProjectDepartment> builder)
        {
            builder.Ignore(i => i.Id);
            builder.HasKey(i => new { ProjectId = i.ProjectId, i.DepartmentId });
        }
    }

    public class ProjectContragentEntityTypeConfiguration : IEntityTypeConfiguration<ProjectContragent>
    {
        public void Configure(EntityTypeBuilder<ProjectContragent> builder)
        {
            builder.Ignore(i => i.Id);
            builder.HasKey(i => new { ProjectId = i.ProjectId, i.ContragentId });
        }
    }

    public class ProjectProductEntityTypeConfiguration : IEntityTypeConfiguration<ProjectProduct>
    {
        public void Configure(EntityTypeBuilder<ProjectProduct> builder)
        {
            builder.Ignore(i => i.Id);
            builder.HasKey(i => new { ProjectId = i.ProjectId, i.ProductId });
        }
    }
}