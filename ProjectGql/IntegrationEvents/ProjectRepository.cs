using System.Collections.Generic;
using System.Threading.Tasks;
using PMIS.Contracts;

namespace PMIS.ProjectGql.IntegrationEvents
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

        public async Task<List<Project>> Save(List<Project> projects)
        {
            await base.AddRange(projects);
            return projects;
        }
    }
}