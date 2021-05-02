using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PMIS.Contracts;
using PMIS.EventBus.IntegrationEvents;

namespace PMIS.EventBus
{
    public class ProjectChangedConsumer : IConsumer<IProjectChangedMessage>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ProjectChangedConsumer> _logger;
        public ProjectChangedConsumer(IServiceProvider serviceProvider, ILogger<ProjectChangedConsumer> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<IProjectChangedMessage> context)
        {
            try
            {
                var projectService = _serviceProvider.GetService<IProjectService>();
                var project = await projectService.Save(context.Message);

                await projectService.Publish(project);

                await context.RespondAsync<ProjectAccepted>(new
                {
                    Value = $"Received: {context.Message.MessageId}"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("ProjectChangedConsumerError", ex);
            }

        }
    }
}