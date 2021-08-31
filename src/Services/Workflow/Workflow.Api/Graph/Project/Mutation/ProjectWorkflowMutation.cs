using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Workflow.Api.Domain;
using Workflow.Api.Graph.Project.Types;

namespace Workflow.Api.Graph.Project.Mutation
{
    [ExtendObjectType(Name = "Mutation")]
    public class ProjectWorkflowMutation
    {
        public async Task<TaskPayload> ProjectNew(
            ChangeStatusInput input,
            [Service] IMediator bus,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            await bus.Send(new ProjectNew.Command
            {
                ProjectId = Guid.NewGuid().ToString()
            });

            return new TaskPayload();
        }

        public async Task<TaskPayload> ProjectPrepared(
            ChangeStatusInput input,
            [Service] IMediator bus,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            await bus.Send(new ProjectPrepared.Command
            {
                ProjectId = input.ProjectId.ToString(),
                TaskId = input.TaskId
            });

            return new TaskPayload();
        }

        public async Task<TaskPayload> ProjectClosed(
            ChangeStatusInput input,
            [Service] IMediator bus,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            await bus.Send(new ProjectClosed.Command
            {
                ProjectId = input.ProjectId.ToString(),
                TaskId = input.TaskId
            });

            return new TaskPayload();
        }
    }
}