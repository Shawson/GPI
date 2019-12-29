using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GPI.Api.BackgroundServices
{

    public class QueuedHostedService : BackgroundService
    {
        private readonly ILogger<QueuedHostedService> logger;
        private readonly IBackgroundTaskQueue backgroundTaskQueue;

        public QueuedHostedService(ILogger<QueuedHostedService> logger, IBackgroundTaskQueue backgroundTaskQueue)
        {
            this.logger = logger;
            this.backgroundTaskQueue = backgroundTaskQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation(
                $"Queued Hosted Service is running.{Environment.NewLine}" +
                $"{Environment.NewLine}Tap W to add a work item to the " +
                $"background queue.{Environment.NewLine}");

            await BackgroundProcessing(stoppingToken);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem =
                    await backgroundTaskQueue.DequeueAsync(stoppingToken);

                try
                {
                    logger.LogInformation("Executing backgroundTaskQueue item");
                    await workItem(stoppingToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex,
                        "Error occurred executing {WorkItem}.", nameof(workItem));
                }
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Queued Hosted Service is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
