using GPI.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPI.Data.Configurations
{
    public class PlatformAliasConfiguration : IEntityTypeConfiguration<PlatformAlias>
    {
        public void Configure(EntityTypeBuilder<PlatformAlias> builder)
        {
            builder
                .HasOne(m => m.Platform)
                .WithMany(a => a.PlatformAliases)
                .HasForeignKey(m => m.PlatformId);
        }
    }
}
