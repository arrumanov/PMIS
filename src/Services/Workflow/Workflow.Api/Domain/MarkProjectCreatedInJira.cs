using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Workflow.Api.Bpmn;
using Workflow.Api.DataAccess;

namespace Workflow.Api.Domain
{
    public class MarkProjectCreatedInJira
    {
        public class Command : IRequest<Guid>
        {
            public Guid ProjectId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Guid>
        {
            private readonly WorkflowContext db;
            private readonly BpmnService bpmnService;

            public Handler(WorkflowContext db, BpmnService bpmnService)
            {
                this.db = db;
                this.bpmnService = bpmnService;
            }

            public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
            {
                using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                var project = await db.Projects.FirstAsync(i => i.Id == request.ProjectId, cancellationToken);
                project.MarkProjectCreatedInJira();
                await db.SaveChangesAsync(cancellationToken);
                await bpmnService.SendMessageInvoicePaid(project);
                tx.Complete();
                return project.Id;
            }
        }
    }
}