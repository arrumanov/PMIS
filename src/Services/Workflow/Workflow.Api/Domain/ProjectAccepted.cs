using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Workflow.Api.Bpmn;
using Workflow.Api.DataAccess;

namespace Workflow.Api.Domain
{
    public class ProjectAccepted
    {
        public class Command : IRequest<Unit>
        {
            public string ProjectId { get; set; }
            public string TaskId { get; set; }
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
                using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                var project = await db.Projects.FirstAsync(p => p.Id.ToString() == request.ProjectId, cancellationToken);

                project.Accept();

                await db.SaveChangesAsync(cancellationToken);

                await bpmnService.CompleteTask(request.TaskId, project);

                tx.Complete();

                return Unit.Value;
            }
        }
    }
}