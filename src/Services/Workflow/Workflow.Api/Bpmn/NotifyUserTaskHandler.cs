using System;
using System.Threading.Tasks;
using Camunda.Worker;
using MediatR;
using Workflow.Api.Domain;

namespace Workflow.Api.Bpmn
{
    [HandlerTopics("Topic_NotifyUser", LockDuration = 10_000)]
    public class NotifyUserTaskHandler : ExternalTaskHandler
    {
        private readonly IMediator bus;

        public NotifyUserTaskHandler(IMediator bus)
        {
            this.bus = bus;
        }

        public override async Task<IExecutionResult> Process(ExternalTask externalTask)
        {
            await bus.Send(new NotifyUser.Command
            {
                ObjectWfId = Guid.Parse(externalTask.Variables["objectWfId"].AsString()),
                ProjectId = Guid.Parse(externalTask.Variables["objectId"].AsString())
            });


            return new CompleteResult { };
        }
    }
}