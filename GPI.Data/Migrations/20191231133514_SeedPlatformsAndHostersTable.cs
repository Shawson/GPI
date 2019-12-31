using GPI.Core;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GPI.Data.Migrations
{
    public partial class SeedPlatformsAndHostersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"INSERT INTO Platform (Id, Title) Values ('{GuidHelper.Platforms.None.ToString().ToUpper()}', 'None')");
            migrationBuilder.Sql($"INSERT INTO Platform (Id, Title) Values ('{GuidHelper.Platforms.PC.ToString().ToUpper()}', 'PC')");
            migrationBuilder.Sql($"INSERT INTO Platform (Id, Title) Values ('{GuidHelper.Platforms.NES.ToString().ToUpper()}', 'Nintendo Entertainment System')");
            migrationBuilder.Sql($"INSERT INTO Platform (Id, Title) Values ('{GuidHelper.Platforms.N64.ToString().ToUpper()}', 'Nintendo 64')");
            migrationBuilder.Sql($"INSERT INTO Platform (Id, Title) Values ('{GuidHelper.Platforms.SNES.ToString().ToUpper()}', 'Super Nintendo Entertainment System')");
            migrationBuilder.Sql($"INSERT INTO Platform (Id, Title) Values ('{GuidHelper.Platforms.SegaGenesis.ToString().ToUpper()}', 'Sega Genesis')");
            migrationBuilder.Sql($"INSERT INTO Platform (Id, Title) Values ('{GuidHelper.Platforms.PSX.ToString().ToUpper()}', 'Sony PlayStation')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"TRUNCATE TABLE Platform");
        }
    }
}
