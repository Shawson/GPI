using GPI.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Text;

namespace GPI.Data.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.DisplayName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(m => m.HosterIdentifier)
                .HasMaxLength(512);

            builder
                .HasOne(m => m.Hoster)
                .WithMany(a => a.Games)
                .HasForeignKey(m => m.HosterId);

            builder
                .HasOne(m => m.Platform)
                .WithMany(a => a.Games)
                .HasForeignKey(m => m.PlatformId);

            builder
                .HasMany(m => m.GameAliases)
                .WithOne(a => a.Game)
                .HasForeignKey(m => m.GameId);

            builder
                .ToTable("Game");
        }
    }
}
