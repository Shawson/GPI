using GPI.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GPI.Services.ContentScanners
{
    public interface IContentScanner<TConfigType> where TConfigType : class
    {
        Task<List<GameInfo>> ScanForGames(Dictionary<string, string> config, CancellationToken token);
        Guid HosterIdentifier { get; }
        Guid DefaultPlatformIdentifier { get; }
        TConfigType Settings { get; set; }
    }
}