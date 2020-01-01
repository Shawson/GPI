using GPI.Core.Models.DTOs;
using GPI.Core.Models.Entities;
using GPI.Data.Repositories;
using GPI.Services.BackgroundTasks;
using GPI.Services.ContentHosts;
using GPI.Services.CQRS.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
        private readonly IBackgroundTaskProgressTracker _backgroundTaskProgressTracker;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMediator _mediator;
        private readonly MD5 _md5Hash;

        public ScanForContentHandler(
            ILogger<ScanForContentHandler> logger,
            IBackgroundTaskProgressTracker backgroundTaskProgressTracker,
            IServiceProvider serviceProvider,
            IMediator mediator)
        {
            _logger = logger;
            _backgroundTaskProgressTracker = backgroundTaskProgressTracker;
            _serviceProvider = serviceProvider;
            _mediator = mediator;

            _md5Hash = MD5.Create();
        }

        public async Task<Unit> Handle(ScanForContentRequest request, CancellationToken cancellationToken)
        {
            _backgroundTaskProgressTracker.AddTaskToTrack("Content Scanning", cancellationToken);

            // get all scanners
            var hosters = await _mediator.Send(new HosterGetAllRequest());
            var getCurrentHashes = await _mediator.Send(new GameGetAllHashesRequest());

            var completedHosters = 0;

            foreach (var hosterDefintion in hosters)
            {
                _logger.LogInformation($"Started hoster scan {hosterDefintion.TypeName}");
                try
                {
                    var hoster = await GetHosterFromType(hosterDefintion);

                    var games = await hoster.ScanForGames(cancellationToken);

                    _logger.LogInformation($"Found {games.Count}");

                    if (games.Any())
                    {
                        var gamesToAdd = games
                            .Select(x => new Game
                            {
                                Id = Guid.NewGuid(),
                                DisplayName = x.DisplayTitle,
                                FileLocation = x.FileLocation,
                                HosterContentIdentifier = x.HosterContentIdentifier,
                                HosterId = hoster.HosterIdentifier,
                                PlatformId = x.PlatformId,
                                Hash = GetMd5Hash($"{x.FileLocation}{x.DisplayTitle}{x.PlatformId}")
                            })
                            .Where(x => !getCurrentHashes.Contains(x.Hash))
                            .ToList();

                        _logger.LogInformation($"{gamesToAdd.Count} of these need adding");

                        if (gamesToAdd.Any())
                        {
                            await _mediator.Send(new GameBulkInsertRequest(gamesToAdd));
                        }
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError($"Problem with hoster {hosterDefintion.TypeName} : {ex}");
                }

                completedHosters++;

                _backgroundTaskProgressTracker.UpdateTask("Content Scanning", completedHosters / hosters.Count);
            }
            

            return await Task.FromResult(Unit.Value);
        }

        private async Task<IBasicContentHost> GetHosterFromType(Hoster hosterDefintion)
        {
            var hosterType = Type.GetType(hosterDefintion.TypeName, true);
            var hoster = (IBasicContentHost)_serviceProvider.AsSelf(hosterType);

            var settingsJson = await _mediator.Send(new HosterFetchConfigRequest(hosterDefintion.TypeName));
            hoster.LoadSettingsFromJson(settingsJson);
            return hoster;
        }

        private string GetMd5Hash(string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = _md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
