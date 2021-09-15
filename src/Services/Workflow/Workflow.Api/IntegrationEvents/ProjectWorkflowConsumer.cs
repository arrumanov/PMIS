using System;
using System.Threading.Tasks;
using CrossCutting;
using MassTransit;

namespace Workflow.Api.IntegrationEvents
{
    public class ProjectWorkflowConsumer : IConsumer<ProjectCreated>
    {
        //private readonly IServiceProvider _serviceProvider;
        public ProjectWorkflowConsumer()
        {
            //_serviceProvider = serviceProvider;
        }
        public async Task Consume(ConsumeContext<ProjectCreated> context)
        {
            try
            {
                Console.WriteLine("DWHConsumer successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine("DWHConsumer error", ex);
            }

        }
    }
}