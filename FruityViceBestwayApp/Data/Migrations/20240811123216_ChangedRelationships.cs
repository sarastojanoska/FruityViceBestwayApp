using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FruityViceBestwayApp.Migrations
{
    public partial class ChangedRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nutritions_Fruits_Id",
                table: "Nutritions");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Nutritions",
                newName: "FruitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nutritions_Fruits_FruitId",
                table: "Nutritions",
                column: "FruitId",
                principalTable: "Fruits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nutritions_Fruits_FruitId",
                table: "Nutritions");

            migrationBuilder.RenameColumn(
                name: "FruitId",
                table: "Nutritions",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Nutritions_Fruits_Id",
                table: "Nutritions",
                column: "Id",
                principalTable: "Fruits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
