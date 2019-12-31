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
    public class ScanForNewHostsRequest : IRequest
    {
    }

    public class ScanForNewHostsHandler : IRequestHandler<ScanForNewHostsRequest, Unit>
    {
        private readonly ILogger<ScanForNewHostsHandler> _logger;
        private readonly IRepository<Hoster> _hosterRepository;
        private readonly IBackgroundTaskProgressTracker _backgroundTaskProgressTracker;

        public ScanForNewHostsHandler(
            ILogger<ScanForNewHostsHandler> logger,
            IRepository<Hoster> hosterRepository,
            IBackgroundTaskProgressTracker backgroundTaskProgressTracker)
        {
            _logger = logger;
            _hosterRepository = hosterRepository;
            _backgroundTaskProgressTracker = backgroundTaskProgressTracker;
        }

        public async Task<Unit> Handle(ScanForNewHostsRequest request, CancellationToken cancellationToken)
        {
            var type = typeof(IBasicContentHost);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => p.GetInterfaces().Contains(type));

            var currentHosters = await _hosterRepository.GetAllAsync().ToListAsync();

            foreach (var hosterType in types)
            {
                IBasicContentHost instance = (IBasicContentHost)Activator.CreateInstance(hosterType);

                if (!currentHosters.Any(x => x.Id == instance.HosterIdentifier))
                {
                    await _hosterRepository.AddAsync(new Hoster
                    {
                        Id = instance.HosterIdentifier,
                        Title = instance.Title
                    });
                }
            }

            await _hosterRepository.SaveChanges();

            return await Task.FromResult(Unit.Value);
        }
    }
}
