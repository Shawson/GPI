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
        private readonly IMediator _mediatr;
        private readonly ILogger<AppStartWorker> _logger;
        private readonly IRepository<Game> _gameRepository;
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly IBackgroundTaskProgressTracker _progressTracker;


        public AppStartWorker(
            IMediator mediatr,
            ILogger<AppStartWorker> logger, 
            IRepository<Game> gameRepository,
            IBackgroundTaskQueue taskQueue,
            IBackgroundTaskProgressTracker progressTracker)
        {
            _mediatr = mediatr;
            _logger = logger;
            _gameRepository = gameRepository;
            _taskQueue = taskQueue;
            _progressTracker = progressTracker;
        }

        public async Task DoWork()
        {
            _logger.LogInformation("AppStartWorker Running");

            await _mediatr.Send(new ScanForContentRequest());

            /*
            */
        }
    }
}
