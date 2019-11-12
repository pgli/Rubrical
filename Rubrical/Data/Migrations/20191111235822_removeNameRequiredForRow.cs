using Microsoft.EntityFrameworkCore.Migrations;

namespace Rubrical.Data.Migrations
{
    public partial class removeNameRequiredForRow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Rows",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Rows",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
