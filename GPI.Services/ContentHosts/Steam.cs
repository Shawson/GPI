using GPI.Core;
using GPI.Core.Models.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GPI.Services.ContentHosts
{
    public class Steam : IBasicContentHost
    {
        public Guid HosterIdentifier { get; } = GuidHelper.Hosters.Steam;
        public Guid DefaultPlatformIdentifier { get; } = GuidHelper.Platforms.PC;
        public SteamConfig Settings { get; set; } = null;

        public string Title => "Steam";

        public Steam()
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
