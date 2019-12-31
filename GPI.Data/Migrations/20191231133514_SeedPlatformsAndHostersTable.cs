using GPI.Core;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GPI.Data.Migrations
{
    public partial class SeedPlatformsAndHostersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"INSERT INTO Platform (Id, Title) Values ('{GuidHelper.Platforms.None}', 'None')");
            migrationBuilder.Sql($"INSERT INTO Platform (Id, Title) Values ('{GuidHelper.Platforms.PC}', 'PC')");
            migrationBuilder.Sql($"INSERT INTO Platform (Id, Title) Values ('{GuidHelper.Platforms.NES}', 'Nintendo Entertainment System')");
            migrationBuilder.Sql($"INSERT INTO Platform (Id, Title) Values ('{GuidHelper.Platforms.N64}', 'Nintendo 64')");
            migrationBuilder.Sql($"INSERT INTO Platform (Id, Title) Values ('{GuidHelper.Platforms.SNES}', 'Super Nintendo Entertainment System')");
            migrationBuilder.Sql($"INSERT INTO Platform (Id, Title) Values ('{GuidHelper.Platforms.SegaGenesis}', 'Sega Genesis')");
            migrationBuilder.Sql($"INSERT INTO Platform (Id, Title) Values ('{GuidHelper.Platforms.PSX}', 'Sony PlayStation')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"TRUNCATE TABLE Platform");
        }
    }
}
