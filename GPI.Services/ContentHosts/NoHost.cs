using GPI.Core;
using GPI.Core.Models.DTOs;
using GPI.Services.FileSystem;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GPI.Services.ContentHosts
{
    public class NoHost : IBasicContentHost
    {
        private readonly IDirectoryShim _directory;
        private readonly ILogger<NoHost> _logger;

        public Guid HosterIdentifier { get; } = GuidHelper.Hosters.None;
        public Guid DefaultPlatformIdentifier { get; } = GuidHelper.Platforms.None;
        public FileSystemConfig Settings { get; set; }

        public string Title => "";

        public NoHost()
        {

        }

        public NoHost(
            IDirectoryShim directory, 
            ILogger<NoHost> logger)
        {
            _directory = directory;
            this._logger = logger;
        }

        public Task<List<GameInfo>> ScanForGames(CancellationToken token)
        {
            List<GameInfo> results = new List<GameInfo>();


            foreach (var defintion in Settings.Definitions)
            {
                try
                {
                    _logger.LogInformation($"Scanning {defintion.Path}");

                    var files = _directory.GetFiles(defintion.Path, defintion.Recursive);

                    results.AddRange(files
                        .Select(x => new GameInfo
                        {
                            FileLocation = x,
                            PlatformId = defintion.PlatformId,
                            DisplayTitle = Path.GetFileNameWithoutExtension(x),
                            HosterContentIdentifier = x
                        })
                        .Where(x => defintion.Extensions.Contains(x.FileExtension))
                        .ToList());
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Problem scanning directory {ex}");
                }
            }

            return Task.FromResult(results);
        }

        public Task LaunchGame(Guid gameId)
        {
            throw new NotImplementedException();
        }

        public void LoadSettingsFromJson(string jsonSettings)
        {
            Settings = JsonConvert.DeserializeObject<FileSystemConfig>(jsonSettings);
        }
    }

    public class FileSystemConfig
    {
        public List<FolderDefinition> Definitions { get; set; }
    }

    public class FolderDefinition
    {
        public string Path { get; set; }
        public List<string> Extensions { get; set; }
        public Guid PlatformId { get; set; }
        public bool Recursive { get; set; }
    }
}