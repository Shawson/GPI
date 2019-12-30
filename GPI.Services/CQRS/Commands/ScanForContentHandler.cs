using GPI.Services.BackgroundTasks;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GPI.Services.CQRS.Commands
{
    public class ScanForContentRequest : IRequest<Unit>
    {
    }

    public class ScanForContentHandler : IRequestHandler<ScanForContentRequest, Unit>
    {
        private readonly ILogger<ScanForContentHandler> _logger;
        private readonly IBackgroundTaskQueue _backgroundTaskQueue;
        private readonly IBackgroundTaskProgressTracker _backgroundTaskProgressTracker;

        public ScanForContentHandler(
            ILogger<ScanForContentHandler> logger,
            IBackgroundTaskQueue backgroundTaskQueue,
            IBackgroundTaskProgressTracker backgroundTaskProgressTracker)
        {
            _logger = logger;
            _backgroundTaskQueue = backgroundTaskQueue;
           _backgroundTaskProgressTracker = backgroundTaskProgressTracker;
        }


        public Task<Unit> Handle(ScanForContentRequest request, CancellationToken cancellationToken)
        {
            _backgroundTaskQueue.QueueBackgroundWorkItem(async token =>
            {
                // Simulate three 5-second tasks to complete
                // for each enqueued work item

                int delayLoop = 0;
                var guid = Guid.NewGuid().ToString();

                _backgroundTaskProgressTracker.AddTaskToTrack("Content Scanning", token);

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

                    _backgroundTaskProgressTracker.UpdateTask("Content Scanning", (decimal)delayLoop / (decimal)3);

                    _logger.LogInformation(
                        "Fake Content Scanner {Guid} is running. " +
                        "{DelayLoop}/3", guid, delayLoop);
                }

                if (delayLoop == 3)
                {
                    _logger.LogInformation(
                        "Fake Content Scanner {Guid} is complete.", guid);
                }
                else
                {
                    _logger.LogInformation(
                        "Fake Content Scanner {Guid} was cancelled.", guid);
                }
            });

            return Task.FromResult(Unit.Value);
        }
    }
}
