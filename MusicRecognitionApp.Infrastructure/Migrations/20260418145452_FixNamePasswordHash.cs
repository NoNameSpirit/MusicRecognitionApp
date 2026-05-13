using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicRecognitionApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixNamePasswordHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasaswordHash",
                table: "Users",
                newName: "PasswordHash");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "PasaswordHash");
        }
    }
}
