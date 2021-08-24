using System;

namespace ProjectPortfolio.Application.Graph.Project.Types
{
    public record AddProjectInput(string Name, string Description, Guid DepartmentId, Guid ContragentId, Guid ProductId);
}