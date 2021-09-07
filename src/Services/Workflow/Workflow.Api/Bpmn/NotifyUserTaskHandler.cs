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
                ProjectId = Guid.Parse(externalTask.Variables["projectId"].AsString())
            });


            return new CompleteResult { };
        }
    }
}