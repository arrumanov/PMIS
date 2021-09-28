using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Workflow.Api.DataAccess;

namespace Workflow.Api.Domain
{
    public class NotifyUser
    {
        public class Command : IRequest<Unit>
        {
            public Guid ObjectWfId { get; set; }
            public Guid ProjectId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly WorkflowContext db;

            public Handler(WorkflowContext db)
            {
                this.db = db;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                var projectWf = await db.ProjectWfs.FirstOrDefaultAsync(p => p.Id == request.ObjectWfId, cancellationToken);

                var (notificationText, targetGroup) = projectWf.Status switch
                {
                    ProjectStatus.Rejected =>
                        ($"Your projectWf {projectWf.Id} be rejected", "User"),
                    _ =>
                        ("", "")
                };

                db.Notifications.Add(new Notification(notificationText, targetGroup, null));

                await db.SaveChangesAsync(cancellationToken);

                tx.Complete();

                return Unit.Value;
            }
        }
    }
}