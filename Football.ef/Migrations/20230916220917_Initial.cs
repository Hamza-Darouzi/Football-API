﻿using System;
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
                name: "Club",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FoundingDate = table.Column<DateTime>(type: "date", nullable: false),
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
                    { 2, "Premiere League" }
                });

            migrationBuilder.InsertData(
                table: "Club",
                columns: new[] { "Id", "FoundingDate", "LeagueId", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1899, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Fc Barcelona" },
                    { 2, new DateTime(1902, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Real Madrid" }
                });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "Id", "BirthYear", "ClubId", "Name", "Nation" },
                values: new object[,]
                {
                    { 1, new DateTime(1987, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Lioniel Messi", "" },
                    { 2, new DateTime(1984, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Andres Iniesta", "" },
                    { 3, new DateTime(1980, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Xavi Hernandez", "" },
                    { 4, new DateTime(1985, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Cristiano Ronaldo", "" },
                    { 5, new DateTime(1987, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Karim Benzema", "" },
                    { 6, new DateTime(1982, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "KAKA", "" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Club_LeagueId",
                table: "Club",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_ClubId",
                table: "Player",
                column: "ClubId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Club");

            migrationBuilder.DropTable(
                name: "League");
        }
    }
}
