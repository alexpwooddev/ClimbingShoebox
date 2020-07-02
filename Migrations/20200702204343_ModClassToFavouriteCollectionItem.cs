using Microsoft.EntityFrameworkCore.Migrations;

namespace ClimbingShoebox.Migrations
{
    public partial class ModClassToFavouriteCollectionItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropTable(
                name: "FavouriteShoes");
            */
            migrationBuilder.CreateTable(
                name: "FavouritesCollectionItems",
                columns: table => new
                {
                    FavouritesCollectionItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoeId = table.Column<int>(nullable: false),
                    FavouritesCollectionId = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouritesCollectionItems", x => x.FavouritesCollectionItemId);
                    table.ForeignKey(
                        name: "FK_FavouritesCollectionItems_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FavouritesCollectionItems_Shoes_ShoeId",
                        column: x => x.ShoeId,
                        principalTable: "Shoes",
                        principalColumn: "ShoeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavouritesCollectionItems_ApplicationUserId",
                table: "FavouritesCollectionItems",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouritesCollectionItems_ShoeId",
                table: "FavouritesCollectionItems",
                column: "ShoeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavouritesCollectionItems");

            migrationBuilder.CreateTable(
                name: "FavouriteShoes",
                columns: table => new
                {
                    FavouriteShoeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ShoeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteShoes", x => x.FavouriteShoeId);
                    table.ForeignKey(
                        name: "FK_FavouriteShoes_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FavouriteShoes_Shoes_ShoeId",
                        column: x => x.ShoeId,
                        principalTable: "Shoes",
                        principalColumn: "ShoeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteShoes_ApplicationUserId",
                table: "FavouriteShoes",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteShoes_ShoeId",
                table: "FavouriteShoes",
                column: "ShoeId");
        }
    }
}
