using System.Collections.Generic;
using System.Threading.Tasks;
using PMIS.Contracts;

namespace PMIS.ProjectGql.IntegrationEvents
{
    public interface IProjectIndexService
    {
        Task IndexProject(List<Project> projects);
    }
}