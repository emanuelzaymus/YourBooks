using Microsoft.EntityFrameworkCore.Migrations;

namespace YourBooks.Migrations
{
    public partial class ImgAndWebURLs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgURL",
                table: "Book",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebURL",
                table: "Book",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgURL",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "WebURL",
                table: "Book");
        }
    }
}
