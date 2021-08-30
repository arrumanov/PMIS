using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using StackExchange.Redis;
using Workflow.Api.Bpmn;
using Workflow.Api.DataAccess;

namespace Workflow.Api.Domain
{
    public class ProjectNew
    {
        public class Command : IRequest<Unit>
        {
            public string ProjectId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly WorkflowContext db;
            private readonly BpmnService bpmnService;

            public Handler(WorkflowContext db, BpmnService bpmnService)
            {
                this.db = db;
                this.bpmnService = bpmnService;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var newProject = new Project("");

                db.Projects.Add(newProject);
                await db.SaveChangesAsync(cancellationToken);

                var processInstanceId = await bpmnService.StartProcessFor(newProject);
                newProject.AssociateWithProcessInstance(processInstanceId);
                await db.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}