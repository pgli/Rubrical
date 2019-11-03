using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rubrical.Data.Migrations
{
    public partial class removedPrivacy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rubrics_Privacies_PrivacyId",
                table: "Rubrics");

            migrationBuilder.DropTable(
                name: "Privacies");

            migrationBuilder.DropIndex(
                name: "IX_Rubrics_PrivacyId",
                table: "Rubrics");

            migrationBuilder.DropColumn(
                name: "PrivacyId",
                table: "Rubrics");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrivacyId",
                table: "Rubrics",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Privacies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PrivacyDescription = table.Column<string>(nullable: true),
                    PrivacyName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privacies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rubrics_PrivacyId",
                table: "Rubrics",
                column: "PrivacyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rubrics_Privacies_PrivacyId",
                table: "Rubrics",
                column: "PrivacyId",
                principalTable: "Privacies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
