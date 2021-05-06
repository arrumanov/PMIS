using Dogovor.Infrastructure.Database.Query.Model.Project;

namespace Dogovor.Infrastructure.Database.Query.Manager
{
    public class ProjectManager : EntityManager<Project>
    {
        public ProjectManager(IManager<Project> manager) : base(manager)
        {
        }
    }
}