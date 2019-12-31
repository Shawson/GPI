using System;
using System.Collections.Generic;

namespace GPI.Core.Models.Entities
{
    public class Game: BaseEntity
    {
        public string DisplayName { get; set; }

        /// <summary>
        /// Full path to games executable/ main data file.  Eg; 
        /// D:\Program Files\Rockstar Games\Red Dead Redemption 2\RDR2.exe or 
        /// E:\Emulation\RetroWinDev\roms\snes\Street Fighter Alpha 2 (USA).zip
        /// </summary>
        public string FileLocation { get; set; }

        /// <summary>
        /// The identifier used by the platform to identify the game.  Eg; SteamId
        /// </summary>
        public string HosterContentIdentifier { get; set; }

        /// <summary>
        /// Used to identify the platform for this game Eg; PC/ SNES/ OCULUS
        /// </summary>
        public Guid PlatformId { get; set; }

        /// <summary>
        /// Identifies the content hoster Eg; Steam/ Oculus/ UPlay/ None (File System)
        /// </summary>
        public Guid HosterId { get; set; }

        public Hoster Hoster { get; set; }
        public Platform Platform { get; set; }
        public IEnumerable<GameAlias> GameAliases { get; set; }
        public string Hash { get; set; }
    }
}
