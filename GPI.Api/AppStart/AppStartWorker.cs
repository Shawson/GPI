using GPI.Core.Models.Entities;
using GPI.Data.Repositories;
using GPI.Services.BackgroundTasks;
using GPI.Services.CQRS.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GPI.Api.AppStart
{
    internal class AppStartWorker : IApplicationStartWorker
    {
        private readonly ILogger<AppStartWorker> _logger;
        private readonly IBackgroundMediatrTaskQueue _mediatrTaskQueue;
        private readonly IBackgroundTaskProgressTracker _progressTracker;


        public AppStartWorker(
            ILogger<AppStartWorker> logger, 
            IBackgroundTaskProgressTracker progressTracker,
            IBackgroundMediatrTaskQueue mediatrTaskQueue)
        {
            _logger = logger;
            _progressTracker = progressTracker;
            _mediatrTaskQueue = mediatrTaskQueue;
        }

        public async Task DoWork()
        {
            _logger.LogInformation("AppStartWorker Running");

            try
            {
                _mediatrTaskQueue.QueueBackgroundWorkItem(new ScanForNewHostsRequest());
                _mediatrTaskQueue.QueueBackgroundWorkItem(new ScanForContentRequest());
                _mediatrTaskQueue.QueueBackgroundWorkItem(new FakeLongRunningProcessRequest());
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Problem queueing startup jobs in AppStart");
            }
        }
    }
}
