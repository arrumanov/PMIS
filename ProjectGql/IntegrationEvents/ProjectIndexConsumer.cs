using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PMIS.Contracts;

namespace PMIS.ProjectGql.IntegrationEvents
{
    public class ProjectIndexConsumer : IConsumer<IProjectDetailedMessage>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ProjectIndexConsumer> _logger;
        public ProjectIndexConsumer(IServiceProvider serviceProvider, ILogger<ProjectIndexConsumer> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<IProjectDetailedMessage> context)
        {
            try
            {
                var indexService = _serviceProvider.GetService<IProjectIndexService>();
                await indexService.IndexProject(new List<Project>() { context.Message.Project });

                await context.RespondAsync<ProjectSavedMessageAcceptedFromSearchIndex>(new
                {
                    Value = $"Received: {context.Message.MessageId}"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("ProjectListConsumer error", ex);
            }

        }
    }
}