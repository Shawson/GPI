using GPI.Core;
using GPI.Core.Models.DTOs;
using GPI.Services.FileSystem;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GPI.Services.ContentHosts
{
    public class SteamHost : IBasicContentHost
    {
        public Guid HosterIdentifier { get; } = GuidHelper.Hosters.Steam;
        public Guid DefaultPlatformIdentifier { get; } = GuidHelper.Platforms.PC;
        public SteamConfig Settings { get; set; } = null;

        public string Title => "Steam";

        public SteamHost() { }

        public SteamHost(IDirectoryShim directory,
            ILogger<SteamHost> logger)
        {

        }

        public Task<List<GameInfo>> ScanForGames(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task LaunchGame(Guid gameId)
        {
            throw new NotImplementedException();
        }

        public void LoadSettingsFromJson(string jsonSettings)
        {
            Settings = JsonConvert.DeserializeObject<SteamConfig>(jsonSettings);
        }
    }

    public class SteamConfig
    {

    }
}