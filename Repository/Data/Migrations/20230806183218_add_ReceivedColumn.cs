using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CentralLibrary.Data.Migrations
{
    public partial class add_ReceivedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Received",
                table: "BookLoans",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Received",
                table: "BookLoans");
        }
    }
}
