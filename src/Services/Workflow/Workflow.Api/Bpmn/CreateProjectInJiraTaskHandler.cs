using System;
using System.Threading.Tasks;
using Camunda.Worker;
using MediatR;
using Workflow.Api.Domain;

namespace Workflow.Api.Bpmn
{
    [HandlerTopics("Topic_CreateProjectInJira", LockDuration = 10_000)]
    public class CreateProjectInJiraTaskHandler : ExternalTaskHandler
    {
        private readonly IMediator bus;

        public CreateProjectInJiraTaskHandler(IMediator bus)
        {
            this.bus = bus;
        }

        public override async Task<IExecutionResult> Process(ExternalTask externalTask)
        {
            var projectId = await bus.Send(new CreateProjectInJira.Command
            {
                ProjectId = Guid.Parse(externalTask.Variables["projectId"].AsString())
            });

            return new CompleteResult { };
            //{
            //    Variables = new Dictionary<string, Variable>
            //    {
            //        ["projectId"] = new Variable(projectId.ToString(), VariableType.String)
            //    }
            //};
        }
    }
}