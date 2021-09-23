using System;
using System.Threading.Tasks;
using CrossCutting;
using MassTransit;
using MassTransit.Definition;
using MediatR;
using Workflow.Api.Domain;

namespace Workflow.Api.IntegrationEvents
{
    public class ProjectWorkflowConsumer : IConsumer<ProjectCreated>
    {
        private readonly IServiceProvider _ServiceProvider;
        private readonly IMediator _Bus;
        public ProjectWorkflowConsumer(IServiceProvider serviceProvider, IMediator bus)
        {
            _ServiceProvider = serviceProvider;
            _Bus = bus;
        }
        public async Task Consume(ConsumeContext<ProjectCreated> context)
        {
            try
            {
                await _Bus.Send(new ProjectNew.Command
                {
                    ProjectId = context.Message.Id.ToString()
                });
                Console.WriteLine("DWHConsumer successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine("DWHConsumer error", ex);
            }

        }
    }

    public class ProjectWorkflowConsumerDefinition :
        ConsumerDefinition<ProjectWorkflowConsumer>
    {
        public ProjectWorkflowConsumerDefinition()
        {
            // override the default endpoint name
            EndpointName = "ProjectCreatedWorkflowQueue";
        }
    }
}