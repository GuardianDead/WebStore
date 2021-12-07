using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WebStore.Migrations
{
    public partial class ОчередныеПравкиВремен : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LifeTime",
                table: "ProductsSold");

            migrationBuilder.DropColumn(
                name: "Guarantee",
                table: "ProductModels");

            migrationBuilder.DropColumn(
                name: "ApproximateDeliveryTime",
                table: "Deliveries");

            migrationBuilder.AddColumn<int>(
                name: "DaysLifeTime",
                table: "ProductsSold",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DaysGuarantee",
                table: "ProductModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApproximateDaysDelivery",
                table: "Deliveries",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysLifeTime",
                table: "ProductsSold");

            migrationBuilder.DropColumn(
                name: "DaysGuarantee",
                table: "ProductModels");

            migrationBuilder.DropColumn(
                name: "ApproximateDaysDelivery",
                table: "Deliveries");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "LifeTime",
                table: "ProductsSold",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Guarantee",
                table: "ProductModels",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ApproximateDeliveryTime",
                table: "Deliveries",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
