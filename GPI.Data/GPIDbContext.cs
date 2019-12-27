using GPI.Core.Models.Entities;
using GPI.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace GPI.Data
{
    public class GPIDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Hoster> Hosters { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<GameAlias> GameAliases { get; set; }
        public DbSet<PlatformAlias> PlatformAliases { get; set; }
        public DbSet<ThirdParty> ThirdParties { get; set; }
        public DbSet<ContentScanner> ContentScanners { get; set; }

        public GPIDbContext(DbContextOptions<GPIDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new GameConfiguration());
            builder.ApplyConfiguration(new HosterConfiguration());
            builder.ApplyConfiguration(new PlatformConfiguration());
            builder.ApplyConfiguration(new AliasConfiguration());
            builder.ApplyConfiguration(new GameAliasConfiguration());
            builder.ApplyConfiguration(new PlatformAliasConfiguration());
        }
    }
}
