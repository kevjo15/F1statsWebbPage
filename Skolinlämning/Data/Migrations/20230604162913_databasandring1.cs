using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skolinlämning.Data.Migrations
{
    /// <inheritdoc />
    public partial class databasandring1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Bloggs");

            migrationBuilder.DropColumn(
                name: "Datetime",
                table: "Bloggs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Bloggs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Datetime",
                table: "Bloggs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
