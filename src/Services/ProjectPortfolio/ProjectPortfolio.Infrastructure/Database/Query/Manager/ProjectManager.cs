using ProjectPortfolio.Infrastructure.Database.Query.Model.Project;

namespace ProjectPortfolio.Infrastructure.Database.Query.Manager
{
    public class ProjectManager : EntityManager<Project>
    {
        public ProjectManager(IManager<Project> manager) : base(manager)
        {
        }
    }
}