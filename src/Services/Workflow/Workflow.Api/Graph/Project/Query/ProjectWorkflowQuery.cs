using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Workflow.Api.Domain;
using Workflow.Api.Graph.Project.Types;

namespace Workflow.Api.Graph.Project.Query
{
    [ExtendObjectType(Name = "Query")]
    public class ProjectWorkflowQuery
    {
        public async Task<ICollection<TaskPayload>> GetProjectTask(
            [Service] IMediator bus,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            var tasks = await bus.Send(new GetProjectTasks.Query
            {
                SalesmanLogin = "" //User.Identity.Name
            });
            return tasks;
        }
    }
}