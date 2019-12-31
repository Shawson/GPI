using GPI.Core.Models.Entities;
using GPI.Data.Repositories;
using GPI.Services.BackgroundTasks;
using GPI.Services.ContentHosts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GPI.Services.CQRS.Commands
{
    public class ScanForContentRequest : IRequest
    {
    }

    public class ScanForContentHandler : IRequestHandler<ScanForContentRequest, Unit>
    {
        private readonly ILogger<ScanForContentHandler> _logger;
        private readonly IRepository<Hoster> _hosterRepository;
        private readonly IBackgroundTaskProgressTracker _backgroundTaskProgressTracker;
        private readonly IServiceProvider _serviceProvider;

        public ScanForContentHandler(
            ILogger<ScanForContentHandler> logger,
            IRepository<Hoster> hosterRepository,
            IBackgroundTaskProgressTracker backgroundTaskProgressTracker,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _hosterRepository = hosterRepository;
            _backgroundTaskProgressTracker = backgroundTaskProgressTracker;
            _serviceProvider = serviceProvider;
        }

        public async Task<Unit> Handle(ScanForContentRequest request, CancellationToken cancellationToken)
        {
            _backgroundTaskProgressTracker.AddTaskToTrack("Content Scanning", cancellationToken);

            // get all scanners
            var hosters = await _hosterRepository.GetAllAsync().ToListAsync();
            var completedHosters = 0;

            foreach(var hosterDefintion in hosters)
            {
                _logger.LogInformation($"Launching hoster {hosterDefintion.Title} {hosterDefintion.TypeName}");

                var hosterType = Type.GetType(hosterDefintion.TypeName, true);
                var hoster = (IBasicContentHost)_serviceProvider.AsSelf(hosterType);


                await hoster.ScanForGames(cancellationToken);

                completedHosters++;

                _backgroundTaskProgressTracker.UpdateTask("Content Scanning", completedHosters / hosters.Count);
            }
            // load and deserialise their json config files

            return await Task.FromResult(Unit.Value);
        }
    }
}
