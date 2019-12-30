using System;
using System.Collections.Generic;

namespace GPI.Core.Models.Entities
{
    public class Launcher : BaseEntity
    {
        public string Title { get; set; }
        public string LauncherExe { get; set; }
        public string LauncherParameters { get; set; }
        public Guid HosterId { get; set; }
        public Guid PlatformId { get; set; }
        public Hoster Hoster { get; set; }
        public Platform Platform { get; set; }
    }
}
