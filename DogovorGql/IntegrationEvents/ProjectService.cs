using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using PMIS.Contracts;

namespace PMIS.DogovorGql.IntegrationEvents
{
    public class ProjectService : IProjectService
    {
        private readonly ILogger<ProjectService> _logger;
        private readonly IPublishEndpoint _endpoint;
        private readonly Appsettings _appsettings;


        public ProjectService(ILogger<ProjectService> logger, IPublishEndpoint endpoint, Appsettings appsettings)
        {
            _logger = logger;
            _endpoint = endpoint;
            _appsettings = appsettings;
        }

        public async Task Put(ProjectDto request, CancellationToken cancellationToken)
        {
            try
            {
                await _endpoint.Publish<IProjectChangedMessage>(new ProjectChangedMessage()
                {
                    MessageId = new Guid(),
                    Project = request,
                    CreationDate = DateTime.Now

                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}