using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Workflow.Api.Bpmn;
using Workflow.Api.DataAccess;
using Workflow.Api.Graph.Project.Types;

namespace Workflow.Api.Domain
{
    public class GetProjectTasks
    {
        public class Query : IRequest<ICollection<TaskPayload>>
        {
            public string UserLogin { get; set; }
        }

        public class Handler : IRequestHandler<Query, ICollection<TaskPayload>>
        {
            private readonly BpmnService bpmnService;
            private readonly WorkflowContext db;

            public Handler(WorkflowContext db, BpmnService bpmnService)
            {
                this.db = db;
                this.bpmnService = bpmnService;
            }

            public async Task<ICollection<TaskPayload>> Handle(Query request, CancellationToken cancellationToken)
            {
                var tasks = await bpmnService.GetTasksForCandidateGroup("Sales", request.UserLogin);
                var processIds = tasks.Select(t => t.ProcessInstanceId).ToList();

                var projectWfs = await db.ProjectWfs
                    .Where(p => processIds.Contains(p.ProcessInstanceId))
                    .ToListAsync(cancellationToken: cancellationToken);
                var processIdToProjectMap = projectWfs.ToDictionary(o => o.ProcessInstanceId, o => o);

                return (from task in tasks
                        let relatedProjectWf = processIdToProjectMap.ContainsKey(task.ProcessInstanceId) ? processIdToProjectMap[task.ProcessInstanceId] : null
                        select TaskPayload.FromEntity(task, relatedProjectWf))
                    .ToList();
            }
        }
    }
}