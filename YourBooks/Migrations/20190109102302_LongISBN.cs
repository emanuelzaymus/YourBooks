using Microsoft.EntityFrameworkCore.Migrations;

namespace YourBooks.Migrations
{
    public partial class LongISBN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ISBN",
                table: "Book",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ISBN",
                table: "Book",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
