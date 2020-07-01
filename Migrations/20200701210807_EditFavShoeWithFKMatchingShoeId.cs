using Microsoft.EntityFrameworkCore.Migrations;

namespace ClimbingShoebox.Migrations
{
    public partial class EditFavShoeWithFKMatchingShoeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteShoes_Shoes_ShoeId",
                table: "FavouriteShoes");

            migrationBuilder.AlterColumn<int>(
                name: "ShoeId",
                table: "FavouriteShoes",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteShoes_Shoes_ShoeId",
                table: "FavouriteShoes",
                column: "ShoeId",
                principalTable: "Shoes",
                principalColumn: "ShoeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteShoes_Shoes_ShoeId",
                table: "FavouriteShoes");

            migrationBuilder.AlterColumn<int>(
                name: "ShoeId",
                table: "FavouriteShoes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteShoes_Shoes_ShoeId",
                table: "FavouriteShoes",
                column: "ShoeId",
                principalTable: "Shoes",
                principalColumn: "ShoeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
