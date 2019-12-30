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
            migrationBuilder.Sql($"INSERT INTO Platform (Id, Title) Values ('{GuidHelper.Platforms.SNES}', 'Super Nintendo Entertainment System')");
            migrationBuilder.Sql($"INSERT INTO Platform (Id, Title) Values ('{GuidHelper.Platforms.SegaGenesis}', 'Sega Genesis')");
            migrationBuilder.Sql($"INSERT INTO Platform (Id, Title) Values ('{GuidHelper.Platforms.PSX}', 'Sony PlayStation')");

            migrationBuilder.Sql($"INSERT INTO Hoster (Id, Title) Values ('{GuidHelper.Hosters.None}', 'None')");
            migrationBuilder.Sql($"INSERT INTO Hoster (Id, Title) Values ('{GuidHelper.Hosters.BattleNet}', 'BattleNet')");
            migrationBuilder.Sql($"INSERT INTO Hoster (Id, Title) Values ('{GuidHelper.Hosters.GOG}', 'GOG')");
            migrationBuilder.Sql($"INSERT INTO Hoster (Id, Title) Values ('{GuidHelper.Hosters.Oculus}', 'Oculus')");
            migrationBuilder.Sql($"INSERT INTO Hoster (Id, Title) Values ('{GuidHelper.Hosters.Origin}', 'Origin')");
            migrationBuilder.Sql($"INSERT INTO Hoster (Id, Title) Values ('{GuidHelper.Hosters.RockStarLauncher}', 'RockStarLauncher')");
            migrationBuilder.Sql($"INSERT INTO Hoster (Id, Title) Values ('{GuidHelper.Hosters.Steam}', 'Steam')");
            migrationBuilder.Sql($"INSERT INTO Hoster (Id, Title) Values ('{GuidHelper.Hosters.Uplay}', 'Uplay')");
            migrationBuilder.Sql($"INSERT INTO Hoster (Id, Title) Values ('{GuidHelper.Hosters.VivePort}', 'VivePort')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
