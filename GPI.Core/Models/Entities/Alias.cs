using System;

namespace GPI.Core.Models.Entities
{
    public abstract class Alias : BaseEntity
    {
        public string Title { get; set; }
        public string Identity { get; set; }
        public Guid ThirdPartyId { get; set; }
    }
}
