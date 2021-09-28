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
    public class ProjectPrepared
    {
        public class Command : IRequest<Unit>
        {
            public Guid ObjectWfId { get; set; }
            public Guid ProjectId { get; set; }
            public string TaskId { get; set; }
        }

        public class Handler : IRequestHandler<ProjectPrepared.Command, Unit>
        {
            private readonly WorkflowContext db;
            private readonly BpmnService bpmnService;

            public Handler(WorkflowContext db, BpmnService bpmnService)
            {
                this.db = db;
                this.bpmnService = bpmnService;
            }

            public async Task<Unit> Handle(ProjectPrepared.Command request, CancellationToken cancellationToken)
            {
                using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                var projectWf = await db.ProjectWfs.FirstAsync(p => p.Id == request.ObjectWfId, cancellationToken);

                projectWf.Prepare();

                await db.SaveChangesAsync(cancellationToken);

                //необходимо обязательно удалять Task из Camunda при окончании Пользовательского перехода,
                //иначе не даст перевести в следующий статус в Camunda
                await bpmnService.CompleteTask(request.TaskId, projectWf);

                tx.Complete();

                return Unit.Value;
            }
        }
    }
}