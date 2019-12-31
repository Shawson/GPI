﻿using GPI.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GPI.Services.ContentHosts
{
    public interface IBasicContentHost
    {
        Guid HosterIdentifier { get; }
        Guid DefaultPlatformIdentifier { get; }
        string Title { get; }
        Task<List<GameInfo>> ScanForGames(Dictionary<string, string> config, CancellationToken token);
        Task LaunchGame(Guid gameId);
    }

    public interface IContentHost<TConfigType> : IBasicContentHost where TConfigType : class
    {
        TConfigType Settings { get; set; }
    }
}