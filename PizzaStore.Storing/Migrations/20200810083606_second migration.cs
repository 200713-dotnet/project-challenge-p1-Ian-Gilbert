using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaStore.Storing.Migrations
{
    public partial class secondmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuCrustId",
                table: "MenuPizzas");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuCrustId",
                table: "MenuPizzas",
                column: "CrustId",
                principalTable: "Crusts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuCrustId",
                table: "MenuPizzas");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuCrustId",
                table: "MenuPizzas",
                column: "CrustId",
                principalTable: "Crusts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
