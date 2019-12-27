using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GPI.Data.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hoster",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    LauncherExe = table.Column<string>(nullable: true),
                    LauncherParameters = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hoster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Platform",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platform", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThirdParties",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThirdParties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContentScanners",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    HosterId = table.Column<Guid>(nullable: false),
                    ScannerAssembly = table.Column<string>(nullable: true),
                    ScanPaths = table.Column<string>(nullable: true),
                    Extensions = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentScanners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentScanners_Hoster_HosterId",
                        column: x => x.HosterId,
                        principalTable: "Hoster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: false),
                    FileLocation = table.Column<string>(nullable: true),
                    HosterIdentifier = table.Column<string>(maxLength: 512, nullable: true),
                    ScannerId = table.Column<Guid>(nullable: true),
                    PlatformId = table.Column<Guid>(nullable: false),
                    HosterId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Game_Hoster_HosterId",
                        column: x => x.HosterId,
                        principalTable: "Hoster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Game_Platform_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platform",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Game_ContentScanners_ScannerId",
                        column: x => x.ScannerId,
                        principalTable: "ContentScanners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Alias",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Identity = table.Column<string>(nullable: true),
                    ThirdPartyId = table.Column<Guid>(nullable: false),
                    AliasType = table.Column<int>(nullable: false),
                    GameId = table.Column<Guid>(nullable: true),
                    PlatformId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alias_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alias_Platform_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platform",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alias_GameId",
                table: "Alias",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Alias_PlatformId",
                table: "Alias",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentScanners_HosterId",
                table: "ContentScanners",
                column: "HosterId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_HosterId",
                table: "Game",
                column: "HosterId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_PlatformId",
                table: "Game",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_ScannerId",
                table: "Game",
                column: "ScannerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alias");

            migrationBuilder.DropTable(
                name: "ThirdParties");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Platform");

            migrationBuilder.DropTable(
                name: "ContentScanners");

            migrationBuilder.DropTable(
                name: "Hoster");
        }
    }
}
