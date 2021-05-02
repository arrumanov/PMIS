using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using PMIS.EventBus.IntegrationEvents;

namespace PMIS.EventBus
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBusControl _busControl;
        private readonly IServiceProvider _serviceProvider;
        private readonly QueueSettings _queueSettings;
        public Worker(IServiceProvider serviceProvider, ILogger<Worker> logger, IBusControl busControl, QueueSettings queueSettings)
        {
            _logger = logger;
            _busControl = busControl;
            _serviceProvider = serviceProvider;
            _queueSettings = queueSettings;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var dbContextService = (IDataContext)_serviceProvider.GetService(typeof(IDataContext));
                dbContextService.EnsureDbCreated();

                _logger.LogInformation("DataProcessor started!");


                var projectChangeHandler = _busControl.ConnectReceiveEndpoint(_queueSettings.QueueName, x =>
                {
                    x.Consumer<ProjectChangedConsumer>(_serviceProvider);
                });

                await projectChangeHandler.Ready;
            }
            catch (Exception ex)
            {
                _logger.LogError("DataProcessor cannot be started.", ex);
            }
        }
    }
}
