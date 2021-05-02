using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PMIS.Contracts;

namespace PMIS.EventBus.IntegrationEvents
{
    public class ProjectService : IProjectService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ProjectService> _logger;
        public ProjectService(IServiceProvider serviceProvider, ILogger<ProjectService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task<Project> Save(IProjectChangedMessage project)
        {
            var entity = new Project()
            {
                Name = project.Project.Name,
                Price = project.Project.Price,
                Quantity = project.Project.Quantity,
                LastModifyDate = DateTime.Now,
                CreationDate = DateTime.Now
            };

            var projectRepository = _serviceProvider.GetService<IProjectRepository>();
            await projectRepository.Save(entity);

            return entity;
        }

        public async Task Publish(Project project)
        {
            try
            {
                var publisher = _serviceProvider.GetService<IPublishEndpoint>();

                var projectSavedMessage = new ProjectDetailedMessage()
                {
                    MessageId = new Guid(),
                    Project = project,
                    CreationDate = DateTime.Now
                };

                await publisher.Publish(projectSavedMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError("ProjectServiceProjecerError", ex);
            }
        }
    }
}