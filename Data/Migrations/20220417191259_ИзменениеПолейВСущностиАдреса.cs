using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.Migrations
{
    public partial class ИзменениеПолейВСущностиАдреса : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HouseNumber",
                table: "Addresses",
                newName: "Region");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Region",
                table: "Addresses",
                newName: "HouseNumber");
        }
    }
}
