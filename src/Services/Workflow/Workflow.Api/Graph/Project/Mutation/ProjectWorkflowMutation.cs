using System;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using MediatR;
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
                ProjectId = Guid.NewGuid(),
                ProjectName = string.Empty
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
                ObjectWfId = input.ObjectWfId,
                ProjectId = input.ObjectId,
                TaskId = input.TaskId
            });

            return new TaskPayload();
        }

        public async Task<TaskPayload> ProjectAccepted(
            ChangeStatusInput input,
            [Service] IMediator bus,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            await bus.Send(new ProjectAccepted.Command
            {
                ObjectWfId = input.ObjectWfId,
                ProjectId = input.ObjectId,
                TaskId = input.TaskId
            });

            return new TaskPayload();
        }

        public async Task<TaskPayload> CreateProjectInJira(
            ChangeStatusInput input,
            [Service] IMediator bus,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            await bus.Send(new CreateProjectInJira.Command
            {
                ObjectWfId = input.ObjectWfId,
                ProjectId = input.ObjectId,
                TaskId = input.TaskId
            });

            return new TaskPayload();
        }

        public async Task<TaskPayload> ProjectRejected(
            ChangeStatusInput input,
            [Service] IMediator bus,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            await bus.Send(new ProjectReject.Command
            {
                ObjectWfId = input.ObjectWfId,
                ProjectId = input.ObjectId,
                TaskId = input.TaskId
            });

            return new TaskPayload();
        }

        public async Task<TaskPayload> MarkProjectCreatedInJira(
            ChangeStatusInput input,
            [Service] IMediator bus,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            await bus.Send(new MarkProjectCreatedInJira.Command
            {
                ProjectId = input.ObjectId
            });

            return new TaskPayload();
        }
    }
}