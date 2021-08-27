using System;
using System.Linq;
using System.Reflection;
using ProjectPortfolio.CrossCutting;
using ProjectPortfolio.Infrastructure.Database.Command.Model;
using Microsoft.EntityFrameworkCore;

namespace ProjectPortfolio.Infrastructure.Database.Command
{
    public partial class ProjectPortfolioContext : DbContext
    {
        public ProjectPortfolioContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Status> Statuses { get; set; }

        #region Project

        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }

        #endregion

        #region Department

        public DbSet<Dictionary> Dictionaries { get; set; }
        public DbSet<DictionaryValue> DictionaryValues { get; set; }
        public DbSet<ProjectMetric> ProjectMetrics { get; set; }
        public DbSet<ProjectRisk> ProjectRisks { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}