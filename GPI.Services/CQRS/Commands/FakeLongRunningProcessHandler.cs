using GPI.Services.BackgroundTasks;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GPI.Services.CQRS.Commands
{
    public class FakeLongRunningProcessRequest : IRequest
    {
    }

    public class FakeLongRunningProcessHandler : IRequestHandler<FakeLongRunningProcessRequest, Unit>
    {
        private readonly ILogger<FakeLongRunningProcessHandler> _logger;
        private readonly IBackgroundTaskProgressTracker _backgroundTaskProgressTracker;

        public FakeLongRunningProcessHandler(
            ILogger<FakeLongRunningProcessHandler> logger,
            IBackgroundTaskProgressTracker backgroundTaskProgressTracker)
        {
            _logger = logger;
            _backgroundTaskProgressTracker = backgroundTaskProgressTracker;
        }

        public async Task<Unit> Handle(FakeLongRunningProcessRequest request, CancellationToken cancellationToken)
        {
            // Simulate three 5-second tasks to complete
            // for each enqueued work item

            int delayLoop = 0;
            var guid = Guid.NewGuid().ToString();

            _backgroundTaskProgressTracker.AddTaskToTrack("Content Scanning", cancellationToken);

            _logger.LogInformation(
                "Queued Background Task {Guid} is starting.", guid);

            while (!cancellationToken.IsCancellationRequested && delayLoop < 3)
            {
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
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


            return await Task.FromResult(Unit.Value);
        }
    }
}
