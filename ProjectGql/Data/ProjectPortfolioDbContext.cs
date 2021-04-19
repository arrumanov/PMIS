using Microsoft.EntityFrameworkCore;

namespace PMIS.ProjectGql.Data
{
    public class ProjectPortfolioDbContext : DbContext
    {
        public ProjectPortfolioDbContext(DbContextOptions<ProjectPortfolioDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; } = default!;
    }
}