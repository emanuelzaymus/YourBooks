using Microsoft.EntityFrameworkCore.Migrations;

namespace YourBooks.Migrations
{
    public partial class RatingLen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Rating",
                table: "Book",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 5);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Rating",
                table: "Book",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);
        }
    }
}
