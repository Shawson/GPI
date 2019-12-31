using GPI.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GPI.Data.Configurations
{
    public class HosterConfiguration : IEntityTypeConfiguration<Hoster>
    {
        public void Configure(EntityTypeBuilder<Hoster> builder)
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
                .ToTable("Hoster");
        }
    }
}
