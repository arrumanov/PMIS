using System;
using System.Collections.Generic;

namespace ProjectPortfolio.Application.Graph.Project.Types
{
    public record AddProjectInput(string Name, string Description, Guid ResponsibleDepartmentId, List<Guid> DepartmentIds, List<Guid> ContragentIds, List<Guid> ProductIds);
}