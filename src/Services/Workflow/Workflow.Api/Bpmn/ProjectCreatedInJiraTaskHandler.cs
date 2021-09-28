using System;
using System.Threading.Tasks;
using Camunda.Worker;
using MediatR;
using Workflow.Api.Domain;

namespace Workflow.Api.Bpmn
{
    [HandlerTopics("Topic_ProjectCreatedInJira", LockDuration = 10_000)]
    public class ProjectCreatedInJiraTaskHandler : ExternalTaskHandler
    {
        private readonly IMediator bus;

        public ProjectCreatedInJiraTaskHandler(IMediator bus)
        {
            this.bus = bus;
        }

        public override async Task<IExecutionResult> Process(ExternalTask externalTask)
        {
            await bus.Send(new ProjectCreatedInJira.Command
            {
                ObjectWfId = Guid.Parse(externalTask.Variables["objectWfId"].AsString()),
                ProjectId = Guid.Parse(externalTask.Variables["objectId"].AsString())
            });

            return new CompleteResult { };
        }
    }
}