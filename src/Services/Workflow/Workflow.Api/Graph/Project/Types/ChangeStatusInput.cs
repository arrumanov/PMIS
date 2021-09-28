using System;
using HotChocolate;
using HotChocolate.Types;

namespace Workflow.Api.Graph.Project.Types
{
    public record ChangeStatusInput([GraphQLType(typeof(IdType))] Guid ObjectWfId, [GraphQLType(typeof(IdType))] Guid ObjectId, string TaskId);
}