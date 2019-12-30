using GPI.Api.BackgroundServices;
using GPI.Core.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GPI.Api.Controllers.Sys
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class BackgroundTaskTestController : ControllerBase
    {
        private readonly IBackgroundTaskQueue taskQueue;
        private readonly IBackgroundTaskProgressTracker taskTracker;
        private readonly ILogger<BackgroundTaskTestController> logger;

        public BackgroundTaskTestController(
            IBackgroundTaskQueue taskQueue,
            IBackgroundTaskProgressTracker taskTracker,
            ILogger<BackgroundTaskTestController> logger)
        {
            this.taskQueue = taskQueue;
            this.taskTracker = taskTracker;
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult<List<BackgroundTaskStatus>> Get()
        {
            return taskTracker.GetActiveBackgroundTasks();
        }

        [HttpPost]
        public bool Post()
        {
            taskQueue.QueueBackgroundWorkItem(async token =>
            {
                // Simulate three 5-second tasks to complete
                // for each enqueued work item

                int delayLoop = 0;
                var guid = Guid.NewGuid().ToString();

                taskTracker.ReportWork("DemoTask", 0);

                logger.LogInformation(
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

                    taskTracker.ReportWork("DemoTask", (decimal)delayLoop / (decimal)3);

                    logger.LogInformation(
                        "Queued Background Task {Guid} is running. " +
                        "{DelayLoop}/3", guid, delayLoop);
                }

                if (delayLoop == 3)
                {
                    logger.LogInformation(
                        "Queued Background Task {Guid} is complete.", guid);
                }
                else
                {
                    logger.LogInformation(
                        "Queued Background Task {Guid} was cancelled.", guid);
                }
            });
            return true;
        }
    }
}