using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skolinlämning.Data.Migrations
{
    /// <inheritdoc />
    public partial class Author1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Bloggs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Bloggs");
        }
    }
}
