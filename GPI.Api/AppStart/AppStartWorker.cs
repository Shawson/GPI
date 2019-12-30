using GPI.Api.BackgroundServices;
using GPI.Core.Models.Entities;
using GPI.Data.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GPI.Api.AppStart
{
    internal class AppStartWorker : IApplicationStartWorker
    {
        private readonly ILogger<AppStartWorker> _logger;
        private readonly IRepository<Game> _gameRepository;
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly IBackgroundTaskProgressTracker _progressTracker;

        public AppStartWorker(
            ILogger<AppStartWorker> logger, 
            IRepository<Game> gameRepository,
            IBackgroundTaskQueue taskQueue,
            IBackgroundTaskProgressTracker progressTracker)
        {
            _logger = logger;
            _gameRepository = gameRepository;
            _taskQueue = taskQueue;
            _progressTracker = progressTracker;
        }

        public Task DoWork()
        {
            
            _logger.LogInformation("Running");

            _taskQueue.QueueBackgroundWorkItem(async token =>
            {
                // Simulate three 5-second tasks to complete
                // for each enqueued work item

                int delayLoop = 0;
                var guid = Guid.NewGuid().ToString();

                _progressTracker.AddTaskToTrack("DemoTask", token);

                _logger.LogInformation(
                    "Queued Background Task {Guid} is starting.", guid);

                while (!token.IsCancellationRequested && delayLoop < 3)
                {
                    try
                    {
                        await Task.Delay(TimeSpan.FromSeconds(5), token);
                    }
                    catch (OperationCanceledException)
                    {
                        // Prevent throwing if the Delay is cancelled
                    }

                    delayLoop++;

                    _progressTracker.UpdateTask("DemoTask", (decimal)delayLoop / (decimal)3);

                    _logger.LogInformation(
                        "Queued Background Task {Guid} is running. " +
                        "{DelayLoop}/3", guid, delayLoop);
                }

                if (delayLoop == 3)
                {
                    _logger.LogInformation(
                        "Queued Background Task {Guid} is complete.", guid);
                }
                else
                {
                    _logger.LogInformation(
                        "Queued Background Task {Guid} was cancelled.", guid);
                }
            });

            return null;
        }
    }
}
