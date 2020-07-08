using Microsoft.EntityFrameworkCore.Migrations;

namespace ClimbingShoebox.Migrations
{
    public partial class AddRatingEntryDomainModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RatingEntries",
                columns: table => new
                {
                    RatingEntryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<int>(nullable: false),
                    ShoeId = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingEntries", x => x.RatingEntryId);
                    table.ForeignKey(
                        name: "FK_RatingEntries_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RatingEntries_Shoes_ShoeId",
                        column: x => x.ShoeId,
                        principalTable: "Shoes",
                        principalColumn: "ShoeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RatingEntries_ApplicationUserId",
                table: "RatingEntries",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingEntries_ShoeId",
                table: "RatingEntries",
                column: "ShoeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RatingEntries");
        }
    }
}
