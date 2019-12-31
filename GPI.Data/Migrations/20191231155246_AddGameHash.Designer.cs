﻿// <auto-generated />
using System;
using GPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GPI.Data.Migrations
{
    [DbContext(typeof(GPIDbContext))]
    [Migration("20191231155246_AddGameHash")]
    partial class AddGameHash
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0");

            modelBuilder.Entity("GPI.Core.Models.Entities.Alias", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AliasType")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateAdded")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("datetime()");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("TEXT");

                    b.Property<string>("Identity")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ThirdPartyId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Alias");

                    b.HasDiscriminator<int>("AliasType");
                });

            modelBuilder.Entity("GPI.Core.Models.Entities.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateAdded")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("datetime()");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("FileLocation")
                        .HasColumnType("TEXT");

                    b.Property<string>("Hash")
                        .HasColumnType("TEXT");

                    b.Property<string>("HosterContentIdentifier")
                        .HasColumnType("TEXT")
                        .HasMaxLength(512);

                    b.Property<Guid>("HosterId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PlatformId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("HosterId");

                    b.HasIndex("PlatformId");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("GPI.Core.Models.Entities.Hoster", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateAdded")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("datetime()");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("TypeName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Hoster");
                });

            modelBuilder.Entity("GPI.Core.Models.Entities.Launcher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateAdded")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("datetime()");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("TEXT");

                    b.Property<string>("LauncherExe")
                        .HasColumnType("TEXT");

                    b.Property<string>("LauncherParameters")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PlatformId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("PlatformId");

                    b.ToTable("Launcher");
                });

            modelBuilder.Entity("GPI.Core.Models.Entities.Platform", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateAdded")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("datetime()");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Platform");
                });

            modelBuilder.Entity("GPI.Core.Models.Entities.ThirdParty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ThirdParties");
                });

            modelBuilder.Entity("GPI.Core.Models.Entities.GameAlias", b =>
                {
                    b.HasBaseType("GPI.Core.Models.Entities.Alias");

                    b.Property<Guid>("GameId")
                        .HasColumnType("TEXT");

                    b.HasIndex("GameId");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("GPI.Core.Models.Entities.PlatformAlias", b =>
                {
                    b.HasBaseType("GPI.Core.Models.Entities.Alias");

                    b.Property<Guid>("PlatformId")
                        .HasColumnType("TEXT");

                    b.HasIndex("PlatformId");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("GPI.Core.Models.Entities.Game", b =>
                {
                    b.HasOne("GPI.Core.Models.Entities.Hoster", "Hoster")
                        .WithMany("Games")
                        .HasForeignKey("HosterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GPI.Core.Models.Entities.Platform", "Platform")
                        .WithMany("Games")
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GPI.Core.Models.Entities.Launcher", b =>
                {
                    b.HasOne("GPI.Core.Models.Entities.Platform", "Platform")
                        .WithMany("Launchers")
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GPI.Core.Models.Entities.GameAlias", b =>
                {
                    b.HasOne("GPI.Core.Models.Entities.Game", "Game")
                        .WithMany("GameAliases")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GPI.Core.Models.Entities.PlatformAlias", b =>
                {
                    b.HasOne("GPI.Core.Models.Entities.Platform", "Platform")
                        .WithMany("PlatformAliases")
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
