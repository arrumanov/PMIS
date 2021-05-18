using System;

namespace ProjectPortfolio.Application.Graph.Project
{
    public record UpdateProjectInfoInput(Guid Id, string Description, string LongDescription);
}