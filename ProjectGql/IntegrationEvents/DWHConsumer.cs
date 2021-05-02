using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PMIS.Contracts;

namespace PMIS.ProjectGql.IntegrationEvents
{
    public class DWHConsumer : IConsumer<IProjectDetailedMessage>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DWHConsumer> _logger;
        public DWHConsumer(IServiceProvider serviceProvider, ILogger<DWHConsumer> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<IProjectDetailedMessage> context)
        {
            try
            {
                var projectService = _serviceProvider.GetService<IProjectService>();
                await projectService.Save(new List<Project>() { context.Message.Project });

                await context.RespondAsync<ProjectSavedMessageAcceptedFromDWH>(new
                {
                    Value = $"Received: {context.Message.MessageId}"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("DWHConsumer error", ex);
            }

        }
    }
}