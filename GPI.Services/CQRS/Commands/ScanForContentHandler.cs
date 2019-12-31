using GPI.Core.Models.Entities;
using GPI.Data.Repositories;
using GPI.Services.BackgroundTasks;
using GPI.Services.ContentHosts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
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
        private readonly IRepository<Game> _gameRepository;
        private readonly IBackgroundTaskProgressTracker _backgroundTaskProgressTracker;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMediator _mediator;

        public ScanForContentHandler(
            ILogger<ScanForContentHandler> logger,
            IRepository<Hoster> hosterRepository,
            IRepository<Game> gameRepository,
            IBackgroundTaskProgressTracker backgroundTaskProgressTracker,
            IServiceProvider serviceProvider,
            IMediator mediator)
        {
            _logger = logger;
            _hosterRepository = hosterRepository;
            _gameRepository = gameRepository;
            _backgroundTaskProgressTracker = backgroundTaskProgressTracker;
            _serviceProvider = serviceProvider;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(ScanForContentRequest request, CancellationToken cancellationToken)
        {
            _backgroundTaskProgressTracker.AddTaskToTrack("Content Scanning", cancellationToken);

            // get all scanners
            var hosters = await _hosterRepository.GetAllAsync().ToListAsync();
            var completedHosters = 0;

            foreach(var hosterDefintion in hosters)
            {
                _logger.LogInformation($"Started hoster scan {hosterDefintion.TypeName}");
                try
                {
                    _logger.LogInformation($"Launching hoster {hosterDefintion.Title} {hosterDefintion.TypeName}");

                    var hosterType = Type.GetType(hosterDefintion.TypeName, true);
                    var hoster = (IBasicContentHost)_serviceProvider.AsSelf(hosterType);

                    var settingsJson = await _mediator.Send(new FetchConfigForHosterRequest(hosterDefintion.TypeName));
                    hoster.LoadSettingsFromJson(settingsJson);

                    var games = await hoster.ScanForGames(cancellationToken);

                    _logger.LogInformation($"Found {games.Count}");

                    await _gameRepository.AddRangeAsync(games.Select(x => new Game
                    {
                        Id = Guid.NewGuid(),
                        DisplayName = x.DisplayTitle,
                        FileLocation = x.FileLocation,
                        HosterContentIdentifier = x.HosterContentIdentifier,
                        HosterId = hoster.HosterIdentifier,
                        PlatformId = x.PlatformId,
                    }).ToList());

                    await _gameRepository.SaveChanges();
                }
                catch(Exception ex)
                {
                    _logger.LogError($"Problem with hoster {hosterDefintion.TypeName} : {ex}");
                }

                completedHosters++;

                _backgroundTaskProgressTracker.UpdateTask("Content Scanning", completedHosters / hosters.Count);
            }

            return await Task.FromResult(Unit.Value);
        }
    }
}
