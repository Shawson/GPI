using System;
using System.Collections.Generic;

namespace GPI.Core.Models.Entities
{
    public class ContentScanner : BaseEntity
    {
        public Guid HosterId { get; set; }
        public string ScannerAssembly { get; set; }
        public string ScanPaths { get; set; }
        public string Extensions { get; set; }
        public Hoster Hoster { get; set; }
        public IEnumerable<Game> Games { get; set; }
    }
}