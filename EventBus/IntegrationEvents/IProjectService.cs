using System.Threading.Tasks;
using PMIS.Contracts;

namespace PMIS.EventBus.IntegrationEvents
{
    public interface IProjectService
    {
        Task<Project> Save(IProjectChangedMessage project);

        Task Publish(Project project);
    }
}