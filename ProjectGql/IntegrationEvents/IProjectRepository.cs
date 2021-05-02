using System.Collections.Generic;
using System.Threading.Tasks;
using PMIS.Contracts;

namespace PMIS.ProjectGql.IntegrationEvents
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        Task<Project> Save(Project project);
        Task<List<Project>> Save(List<Project> projects);
    }
}