using System;

namespace GPI.Core.Models.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
