using GPI.Core;
using GPI.Core.Models.DTOs;
using GPI.Services.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GPI.Services.ContentScanners
{
    public class FileSystem : IContentScanner<FileSystemConfig>
    {
        private readonly IDirectoryShim _directory;

        public Guid HosterIdentifier { get; } = GuidHelper.Hosters.None;
        public Guid DefaultPlatformIdentifier { get; } = GuidHelper.Platforms.None;
        public FileSystemConfig Settings { get; set; }

        public FileSystem(IDirectoryShim directory)
        {
            this._directory = directory;
        }

        public Task<List<GameInfo>> ScanForGames(Dictionary<string, string> config, CancellationToken token)
        {
            List<GameInfo> results = new List<GameInfo>();

            foreach (var defintion in Settings.Definitions)
            {
                var files = _directory.GetFiles(defintion.Path, defintion.Recursive);

                results.AddRange(files
                    .Select(x => new GameInfo
                    {
                        FileLocation = x,
                        PlatformId = defintion.PlatformId
                    })
                    .Where(x => defintion.Extensions.Contains(x.FileExtension))
                    .ToList());
            }

            return Task.FromResult(results);
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