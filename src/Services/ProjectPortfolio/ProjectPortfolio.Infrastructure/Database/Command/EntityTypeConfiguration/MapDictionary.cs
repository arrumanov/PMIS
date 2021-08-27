using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectPortfolio.Infrastructure.Database.Command.Model;

namespace ProjectPortfolio.Infrastructure.Database.Command.EntityTypeConfiguration
{
    public class MapDictionary
    {
        public class DictionaryEntityTypeConfiguration : IEntityTypeConfiguration<Dictionary>
        {
            public void Configure(EntityTypeBuilder<Dictionary> builder)
            {
                builder.HasIndex(i => i.DictionaryKey).IsUnique();
                builder.Property(i => i.DictionaryKey).HasMaxLength(100).IsRequired();
                builder.Property(i => i.Name).HasMaxLength(255).IsRequired();
            }
        }

        public class DictionaryValueEntityTypeConfiguration : IEntityTypeConfiguration<DictionaryValue>
        {
            public void Configure(EntityTypeBuilder<DictionaryValue> builder)
            {
                builder.Property(i => i.DictionaryKey).HasMaxLength(100).IsRequired();
                builder.Property(i => i.Name).HasMaxLength(1000).IsRequired();
                builder.Property(i => i.Code).HasMaxLength(100);
            }
        }

        public class ProjectMetricEntityTypeConfiguration : IEntityTypeConfiguration<ProjectMetric>
        {
            public void Configure(EntityTypeBuilder<ProjectMetric> builder)
            {
                builder.Property(i => i.Name).HasMaxLength(1000).IsRequired();
                builder.Property(i => i.Description).HasMaxLength(1000);
            }
        }

        public class ProjectRiskEntityTypeConfiguration : IEntityTypeConfiguration<ProjectRisk>
        {
            public void Configure(EntityTypeBuilder<ProjectRisk> builder)
            {
                builder.Property(i => i.Name).HasMaxLength(1000).IsRequired().IsRequired();
                builder.Property(i => i.ConditionOffensive).HasMaxLength(1000).IsRequired();
                builder.Property(i => i.ProbabilityOffensive).HasMaxLength(1000).IsRequired();
                builder.Property(i => i.DegreeEffect).HasMaxLength(1000).IsRequired();
                builder.Property(i => i.MethodOfResponse).HasMaxLength(1000).IsRequired();
            }
        }
    }
}