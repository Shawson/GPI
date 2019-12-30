﻿using GPI.Api.BackgroundServices;
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
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly IBackgroundTaskProgressTracker _taskTracker;
        private readonly ILogger<BackgroundTaskTestController> _logger;

        public BackgroundTaskTestController(
            IBackgroundTaskQueue taskQueue,
            IBackgroundTaskProgressTracker taskTracker,
            ILogger<BackgroundTaskTestController> logger)
        {
            this._taskQueue = taskQueue;
            this._taskTracker = taskTracker;
            this._logger = logger;
        }

        [HttpGet]
        public ActionResult<List<BackgroundTaskStatus>> Get()
        {
            return _taskTracker.GetActiveBackgroundTasks();
        }

        [HttpPost]
        public bool Post()
        {
            _taskQueue.QueueBackgroundWorkItem(async token =>
            {
                // Simulate three 5-second tasks to complete
                // for each enqueued work item

                int delayLoop = 0;
                var guid = Guid.NewGuid().ToString();

                _taskTracker.AddTaskToTrack("DemoTask", token);

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

                    _taskTracker.UpdateTask("DemoTask", (decimal)delayLoop / (decimal)3);

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
            return true;
        }
    }
}