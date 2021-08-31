using System;

namespace Workflow.Api.Graph.Project.Types
{
    public record ChangeStatusInput(Guid ProjectId, string TaskId);
}