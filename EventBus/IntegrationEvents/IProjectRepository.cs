using System.Threading.Tasks;
using PMIS.Contracts;

namespace PMIS.EventBus.IntegrationEvents
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        Task<Project> Save(Project project);
    }
}