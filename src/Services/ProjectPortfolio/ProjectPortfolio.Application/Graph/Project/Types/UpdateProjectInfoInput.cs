using System;

namespace ProjectPortfolio.Application.Graph.Project.Types
{
    public record UpdateProjectInfoInput(Guid Id, string Description, string LongDescription);
}