using GPI.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GPI.Data.Configurations
{
    public class LauncherConfiguration : IEntityTypeConfiguration<Launcher>
    {
        public void Configure(EntityTypeBuilder<Launcher> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.DateAdded)
                .HasDefaultValueSql("datetime()");

            builder
                .Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasOne(m => m.Platform)
                .WithMany(m => m.Launchers)
                .HasForeignKey(m => m.PlatformId);

            builder
                .ToTable("Launcher");
        }
    }
}
