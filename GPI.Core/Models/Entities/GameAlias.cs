using System;

namespace GPI.Core.Models.Entities
{
    public class GameAlias : Alias
    {
        public Guid GameId { get; set; }
        public Game Game { get; set; }
    }
}
