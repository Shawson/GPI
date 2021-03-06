﻿using System.Collections.Generic;

namespace GPI.Core.Models.Entities
{
    public class Platform: BaseEntity
    {
        public string Title { get; set; }
        public IEnumerable<PlatformAlias> PlatformAliases { get; set; }
        public IEnumerable<Game> Games { get; set; }
        public IEnumerable<Launcher> Launchers { get; set; }
    }


}
