﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Rubrical.Data.Migrations
{
    public partial class addedTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Rubrics",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Rubrics");
        }
    }
}
