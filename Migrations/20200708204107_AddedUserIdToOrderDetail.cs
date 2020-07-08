using Microsoft.EntityFrameworkCore.Migrations;

namespace ClimbingShoebox.Migrations
{
    public partial class AddedUserIdToOrderDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "OrderDetails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ApplicationUserId",
                table: "OrderDetails",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_AspNetUsers_ApplicationUserId",
                table: "OrderDetails",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_AspNetUsers_ApplicationUserId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_ApplicationUserId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "OrderDetails");
        }
    }
}
