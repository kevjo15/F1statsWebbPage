using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skolinlämning.Data.Migrations
{
    /// <inheritdoc />
    public partial class Testrunda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Author_Bloggs_bloggPostID",
                table: "Author");

            migrationBuilder.RenameColumn(
                name: "bloggPostID",
                table: "Author",
                newName: "BloggPostID");

            migrationBuilder.RenameIndex(
                name: "IX_Author_bloggPostID",
                table: "Author",
                newName: "IX_Author_BloggPostID");

            migrationBuilder.AddForeignKey(
                name: "FK_Author_Bloggs_BloggPostID",
                table: "Author",
                column: "BloggPostID",
                principalTable: "Bloggs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Author_Bloggs_BloggPostID",
                table: "Author");

            migrationBuilder.RenameColumn(
                name: "BloggPostID",
                table: "Author",
                newName: "bloggPostID");

            migrationBuilder.RenameIndex(
                name: "IX_Author_BloggPostID",
                table: "Author",
                newName: "IX_Author_bloggPostID");

            migrationBuilder.AddForeignKey(
                name: "FK_Author_Bloggs_bloggPostID",
                table: "Author",
                column: "bloggPostID",
                principalTable: "Bloggs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
