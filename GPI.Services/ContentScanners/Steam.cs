using GPI.Core;
using GPI.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GPI.Services.ContentScanners
{
    public class Steam : IContentScanner<SteamConfig>
    {
        public Guid HosterIdentifier { get; } = GuidHelper.Hosters.Steam;
        public Guid DefaultPlatformIdentifier { get; } = GuidHelper.Platforms.PC;
        public SteamConfig Settings { get; set; } = null;

        public Task<List<GameInfo>> ScanForGames(Dictionary<string, string> config, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }

    public class SteamConfig
    {

    }
}
