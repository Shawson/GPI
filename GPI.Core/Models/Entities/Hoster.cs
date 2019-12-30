using System.Collections.Generic;

namespace GPI.Core.Models.Entities
{
    public class Hoster : BaseEntity
    {
        public string Title { get; set; }
        public IEnumerable<Game> Games { get; set; }
    }
}
