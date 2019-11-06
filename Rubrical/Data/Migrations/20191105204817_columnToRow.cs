using Microsoft.EntityFrameworkCore.Migrations;

namespace Rubrical.Data.Migrations
{
    public partial class columnToRow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cells_Columns_ColumnId",
                table: "Cells");

            migrationBuilder.DropForeignKey(
                name: "FK_Columns_Rubrics_RubricId",
                table: "Columns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Columns",
                table: "Columns");

            migrationBuilder.RenameTable(
                name: "Columns",
                newName: "Rows");

            migrationBuilder.RenameColumn(
                name: "ColumnId",
                table: "Cells",
                newName: "RowId");

            migrationBuilder.RenameIndex(
                name: "IX_Cells_ColumnId",
                table: "Cells",
                newName: "IX_Cells_RowId");

            migrationBuilder.RenameIndex(
                name: "IX_Columns_RubricId",
                table: "Rows",
                newName: "IX_Rows_RubricId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rows",
                table: "Rows",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cells_Rows_RowId",
                table: "Cells",
                column: "RowId",
                principalTable: "Rows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_Rubrics_RubricId",
                table: "Rows",
                column: "RubricId",
                principalTable: "Rubrics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cells_Rows_RowId",
                table: "Cells");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_Rubrics_RubricId",
                table: "Rows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rows",
                table: "Rows");

            migrationBuilder.RenameTable(
                name: "Rows",
                newName: "Columns");

            migrationBuilder.RenameColumn(
                name: "RowId",
                table: "Cells",
                newName: "ColumnId");

            migrationBuilder.RenameIndex(
                name: "IX_Cells_RowId",
                table: "Cells",
                newName: "IX_Cells_ColumnId");

            migrationBuilder.RenameIndex(
                name: "IX_Rows_RubricId",
                table: "Columns",
                newName: "IX_Columns_RubricId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Columns",
                table: "Columns",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cells_Columns_ColumnId",
                table: "Cells",
                column: "ColumnId",
                principalTable: "Columns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Columns_Rubrics_RubricId",
                table: "Columns",
                column: "RubricId",
                principalTable: "Rubrics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
