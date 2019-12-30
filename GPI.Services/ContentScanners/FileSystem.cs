using GPI.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GPI.Services.ContentScanners
{
    public class FileSystem : IContentScanner<FileSystemConfig>
    {
        public Guid HosterIdentifier { get; } = Guid.Parse("E6836EFD-3A8A-4F1E-8D9A-EF0B582268B3");
        public Guid DefaultPlatformIdentifier { get; } = Guid.Parse("E90C3B68-73A4-44B8-9DB2-78045F63E32B");
        public FileSystemConfig Settings { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Task<List<GameInfo>> ScanForGames(Dictionary<string, string> config, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }

    public class FileSystemConfig
    {
        public string Path { get; set; }
        public List<string> Extensions { get; set; }
        public bool Recursive { get; set; }
    }
}