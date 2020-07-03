using Microsoft.EntityFrameworkCore.Migrations;

namespace ClimbingShoebox.Migrations
{
    public partial class RemoveFavouritesCollectionIdFromItemClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavouritesCollectionId",
                table: "FavouritesCollectionItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FavouritesCollectionId",
                table: "FavouritesCollectionItems",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
