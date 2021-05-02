using System.Threading;
using System.Threading.Tasks;
using PMIS.Contracts;

namespace PMIS.DogovorGql.IntegrationEvents
{
    public interface IProjectService
    {
        Task Put(ProjectDto request, CancellationToken cancellationToken);
    }
}