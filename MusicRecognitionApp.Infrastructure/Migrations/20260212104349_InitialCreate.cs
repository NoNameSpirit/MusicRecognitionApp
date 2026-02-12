using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicRecognitionApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Artist = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AudioHashes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<int>(type: "INT", nullable: false),
                    TimeOffset = table.Column<double>(type: "float", nullable: false),
                    SongId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioHashes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AudioHashes_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecognizedSongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecognitionDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Matches = table.Column<int>(type: "int", nullable: false),
                    SongId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecognizedSongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecognizedSongs_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AudioHashes_Hash",
                table: "AudioHashes",
                column: "Hash");

            migrationBuilder.CreateIndex(
                name: "IX_AudioHashes_SongId",
                table: "AudioHashes",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_RecognizedSongs_RecognitionDate",
                table: "RecognizedSongs",
                column: "RecognitionDate");

            migrationBuilder.CreateIndex(
                name: "IX_RecognizedSongs_SongId",
                table: "RecognizedSongs",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_Title_Artist",
                table: "Songs",
                columns: new[] { "Title", "Artist" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AudioHashes");

            migrationBuilder.DropTable(
                name: "RecognizedSongs");

            migrationBuilder.DropTable(
                name: "Songs");
        }
    }
}
