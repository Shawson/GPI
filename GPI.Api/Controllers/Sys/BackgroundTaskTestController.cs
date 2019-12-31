using GPI.Core.Models.DTOs;
using GPI.Services.BackgroundTasks;
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
        private readonly IBackgroundTaskProgressTracker _taskTracker;
        private readonly ILogger<BackgroundTaskTestController> _logger;

        public BackgroundTaskTestController(
            IBackgroundTaskProgressTracker taskTracker,
            ILogger<BackgroundTaskTestController> logger)
        {
            _taskTracker = taskTracker;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<BackgroundTaskStatus>> Get()
        {
            return _taskTracker.GetActiveBackgroundTasks();
        }
    }
}