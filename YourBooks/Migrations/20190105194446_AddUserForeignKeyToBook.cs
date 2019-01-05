using Microsoft.EntityFrameworkCore.Migrations;

namespace YourBooks.Migrations
{
    public partial class AddUserForeignKeyToBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemoveMeLater",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Book",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_UserId",
                table: "Book",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_AspNetUsers_UserId",
                table: "Book",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_AspNetUsers_UserId",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_UserId",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Book");

            migrationBuilder.AddColumn<string>(
                name: "RemoveMeLater",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
