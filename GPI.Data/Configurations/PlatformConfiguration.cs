using GPI.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPI.Data.Configurations
{
    public class PlatformConfiguration : IEntityTypeConfiguration<Platform>
    {
        public void Configure(EntityTypeBuilder<Platform> builder)
        {
            builder
                .HasKey(a => a.Id);
                
            builder
                .Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(50);
            
            builder
                .ToTable("Platform");
        }
    }
}
