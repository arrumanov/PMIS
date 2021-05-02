using System.Threading.Tasks;
using PMIS.Contracts;

namespace PMIS.EventBus.IntegrationEvents
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {

        public ProjectRepository(ConnectionStrings connectionSettings) : base(connectionSettings)
        {

        }

        public async Task<Project> Save(Project project)
        {
            await base.Add(project);
            return project;
        }
    }
}