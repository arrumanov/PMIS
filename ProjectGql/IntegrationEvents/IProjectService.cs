using System.Collections.Generic;
using System.Threading.Tasks;
using PMIS.Contracts;

namespace PMIS.ProjectGql.IntegrationEvents
{
    public interface IProjectService
    {
        Task Save(List<Project> projects);
    }
}