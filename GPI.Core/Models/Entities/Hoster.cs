using System.Collections.Generic;

namespace GPI.Core.Models.Entities
{
    public class Hoster : BaseEntity
    {
        public string Title { get; set; }
        public string LauncherExe { get; set; }
        public string LauncherParameters { get; set; }
        public IEnumerable<Game> Games { get; set; }
    }
}
