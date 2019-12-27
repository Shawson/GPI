using System;

namespace GPI.Core.Models.Entities
{
    public class PlatformAlias : Alias
    {
        public Guid PlatformId { get; set; }
        public Platform Platform { get; set; }
    }
}
