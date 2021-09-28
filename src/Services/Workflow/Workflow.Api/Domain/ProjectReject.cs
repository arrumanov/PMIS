﻿using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Workflow.Api.Bpmn;
using Workflow.Api.DataAccess;

namespace Workflow.Api.Domain
{
    public class ProjectReject
    {
        public class Command : IRequest<Guid>
        {
            public Guid ObjectWfId { get; set; }
            public Guid ProjectId { get; set; }
            public string TaskId { get; set; }
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

                var projectwf = await db.ProjectWfs.FirstAsync(p => p.Id == request.ObjectWfId, cancellationToken);

                projectwf.Reject();

                await db.SaveChangesAsync(cancellationToken);

                await bpmnService.CompleteTask(request.TaskId, projectwf);

                tx.Complete();

                return projectwf.Id;
            }
        }
    }
}