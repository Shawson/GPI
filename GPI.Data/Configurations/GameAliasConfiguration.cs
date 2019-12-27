using GPI.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPI.Data.Configurations
{
    public class GameAliasConfiguration : IEntityTypeConfiguration<GameAlias>
    {
        public void Configure(EntityTypeBuilder<GameAlias> builder)
        {
            builder
                .HasOne(m => m.Game)
                .WithMany(a => a.GameAliases)
                .HasForeignKey(m => m.GameId);
        }
    }
}
