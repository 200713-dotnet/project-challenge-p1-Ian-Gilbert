using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaStore.Storing.Migrations
{
    public partial class eigthmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PurchaseDate",
                table: "Orders",
                type: "datetime2(0)",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 11, 5, 45, 34, 297, DateTimeKind.Utc).AddTicks(2880),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PurchaseDate",
                table: "Orders",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValue: new DateTime(2020, 8, 11, 5, 45, 34, 297, DateTimeKind.Utc).AddTicks(2880));
        }
    }
}
