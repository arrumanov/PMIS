using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Workflow.Api.DataAccess;

namespace Workflow.Api.Domain
{
    public class ProjectNotCreatedInJira
    {
        public class Command : IRequest<Guid>
        {
            public Guid ObjectWfId { get; set; }
            public Guid ProjectId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Guid>
        {
            private readonly WorkflowContext db;

            public Handler(WorkflowContext db)
            {
                this.db = db;
            }

            public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
            {
                using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                var projectWf = await db.ProjectWfs.FirstAsync(p => p.Id == request.ObjectWfId, cancellationToken);

                projectWf.Accept();

                await db.SaveChangesAsync(cancellationToken);

                tx.Complete();

                return projectWf.Id;
            }
        }
    }
}