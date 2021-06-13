using System;

namespace ProjectPortfolio.Application.Graph.Project.Types
{
    public record UserProjectInput(Guid ProjectId, Guid UserId);
}