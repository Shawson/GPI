using GPI.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPI.Data.Configurations
{
    public class AliasConfiguration : IEntityTypeConfiguration<Alias>
    {
        public void Configure(EntityTypeBuilder<Alias> builder)
        {
            builder
                .HasKey(a => a.Id);

            builder
                .HasDiscriminator<int>("AliasType")
                .HasValue<GameAlias>((int)AliasType.Game)
                .HasValue<PlatformAlias>((int)AliasType.Platform);
                
            builder
                .Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(50);
            
            builder
                .ToTable("Alias");
        }
    }
}
