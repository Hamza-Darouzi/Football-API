using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Football.EF.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "League",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_League", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    RefreshToken_ExpireAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefreshToken_Token = table.Column<string>(type: "nvarchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Club",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FoundingDate = table.Column<DateTime>(type: "date", nullable: true),
                    LeagueId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Club", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Club_League_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "League",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BirthYear = table.Column<DateTime>(type: "date", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Player_Club_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Club",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "League",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "La Liga" },
                    { 2, "Premiere League" },
                    { 3, "League 1" },
                    { 4, "Serie A" }
                });

            migrationBuilder.InsertData(
                table: "Club",
                columns: new[] { "Id", "FoundingDate", "LeagueId", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1899, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Fc Barcelona" },
                    { 2, new DateTime(1902, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Real Madrid" },
                    { 3, null, 3, "PSG" },
                    { 4, null, 4, "Napoli" },
                    { 5, null, 2, "Manchester City" },
                    { 6, null, 2, "Liverpool" }
                });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "Id", "BirthYear", "ClubId", "Name", "Nation" },
                values: new object[,]
                {
                    { 1, new DateTime(1987, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Lioniel Messi", "Argentina" },
                    { 2, new DateTime(1984, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Andres Iniesta", "Spain" },
                    { 3, new DateTime(1980, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Xavi Hernandez", "Spain" },
                    { 4, new DateTime(1985, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Cristiano Ronaldo", "Portugal" },
                    { 5, new DateTime(1987, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Karim Benzema", "France" },
                    { 6, new DateTime(1982, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "KAKA", "Brazil" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Club_LeagueId",
                table: "Club",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Club_Name",
                table: "Club",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_League_Name",
                table: "League",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Player_ClubId",
                table: "Player",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_Name",
                table: "Player",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Club");

            migrationBuilder.DropTable(
                name: "League");
        }
    }
}
