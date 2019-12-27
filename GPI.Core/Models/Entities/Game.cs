﻿using System;
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
        public string HosterIdentifier { get; set; }

        /// <summary>
        /// Used to identify scanner used to import this content.  Null if no scanner was used
        /// </summary>
        public Guid? ScannerId { get; set; }
        public Guid PlatformId { get; set; }
        public Guid HosterId { get; set; }

        public ContentScanner ContentScanner { get; set; }
        public Hoster Hoster { get; set; }
        public Platform Platform { get; set; }
        public IEnumerable<GameAlias> GameAliases { get; set; }
        
    }
}
