using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class indextolower : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Movies_Title",
                table: "Movies");

            migrationBuilder.AlterDatabase(
                oldCollation: "NOCASE");

            migrationBuilder.AddColumn<string>(
                name: "TitleToLower",
                table: "Movies",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_TitleToLower",
                table: "Movies",
                column: "TitleToLower");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Movies_TitleToLower",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "TitleToLower",
                table: "Movies");

            migrationBuilder.AlterDatabase(
                collation: "NOCASE");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Title",
                table: "Movies",
                column: "Title");
        }
    }
}
