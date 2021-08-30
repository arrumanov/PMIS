using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Workflow.Api.Domain;

namespace Workflow.Api.Graph.Project.Mutation
{
    [ExtendObjectType(Name = "Mutation")]
    public class ProjectWorkflowMutation
    {
        public async Task ProjectNew(
            [Service] IMediator bus,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            await bus.Send(new ProjectNew.Command
            {
                ProjectId = Guid.NewGuid().ToString()
            });
        }
    }
}