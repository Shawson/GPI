using GPI.Services.BackgroundTasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GPI.Api.BackgroundServices
{
    internal class QueuedHostedMediatrTaskService : BackgroundService
    {
        private readonly ILogger<QueuedHostedMediatrTaskService> _logger;
        private readonly IBackgroundMediatrTaskQueue _backgroundTaskQueue;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public QueuedHostedMediatrTaskService(
            ILogger<QueuedHostedMediatrTaskService> logger, 
            IBackgroundMediatrTaskQueue backgroundTaskQueue,
            IServiceScopeFactory serviceScopeFactory)
        {
            this._logger = logger;
            this._backgroundTaskQueue = backgroundTaskQueue;
            this.serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Mediatr Queued Hosted Service is running.");

            await BackgroundProcessing(stoppingToken);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem =
                    await _backgroundTaskQueue.DequeueAsync(stoppingToken);

                try
                {
                    _logger.LogInformation("Executing backgroundMediatrTaskQueue item");
                    using (var scope = serviceScopeFactory.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetService<IMediator>();
                        await mediator.Send(workItem);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        "Error occurred executing Mediatr {WorkItem}.", nameof(workItem));
                }
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Queued Hosted Mediatr Service is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
