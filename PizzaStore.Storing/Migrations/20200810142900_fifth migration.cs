using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaStore.Storing.Migrations
{
    public partial class fifthmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Orders",
                type: "money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "money");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Orders",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldNullable: true);
        }
    }
}
