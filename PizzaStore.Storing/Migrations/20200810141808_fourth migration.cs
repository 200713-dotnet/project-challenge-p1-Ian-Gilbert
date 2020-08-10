using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaStore.Storing.Migrations
{
    public partial class fourthmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PurchaseDate",
                table: "Orders",
                type: "datetime2(0)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AddColumn<bool>(
                name: "Submitted",
                table: "Orders",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Submitted",
                table: "Orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PurchaseDate",
                table: "Orders",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldNullable: true);
        }
    }
}
