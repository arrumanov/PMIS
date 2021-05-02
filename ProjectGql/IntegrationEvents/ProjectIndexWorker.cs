using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PMIS.ProjectGql.IntegrationEvents
{
    public class ProjectIndexWorker : BackgroundService
    {
        private readonly ILogger<ProjectIndexWorker> _logger;
        private readonly IBusControl _busControl;
        private readonly IServiceProvider _serviceProvider;

        public ProjectIndexWorker(IServiceProvider serviceProvider, IBusControl busControl, ILogger<ProjectIndexWorker> logger)
        {
            _logger = logger;
            _busControl = busControl;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var hostReceiveEndpointHandler = _busControl.ConnectReceiveEndpoint("ProjectListQueue", x =>
                {
                    x.Consumer<ProjectIndexConsumer>(_serviceProvider);
                });

                await hostReceiveEndpointHandler.Ready;
            }
            catch (Exception ex)
            {
                _logger.LogError("DWHConsumer error", ex);
            }
        }
    }
}