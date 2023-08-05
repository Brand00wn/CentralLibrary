using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CentralLibrary.Data.Migrations
{
    public partial class add_bookCreationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Book",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Book");
        }
    }
}
