using System;
using System.Collections.Generic;

namespace GPI.Core.Models.Entities
{
    /// <summary>
    /// A launcher is only required for a game with no Hoster (Eg; File System)
    /// This specifies, in the absense of a Hoster, how the game should be launched
    /// </summary>
    public class Launcher : BaseEntity
    {
        public string Title { get; set; }
        public string LauncherExe { get; set; }
        public string LauncherParameters { get; set; }
        public Guid PlatformId { get; set; }
        public Platform Platform { get; set; }
    }
}
