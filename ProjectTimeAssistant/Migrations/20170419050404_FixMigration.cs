using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTimeAssistant.Migrations
{
    public partial class FixMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "WorkTimes",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Hours",
                table: "WorkTimes",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "WorkTimes");

            migrationBuilder.DropColumn(
                name: "Hours",
                table: "WorkTimes");
        }
    }
}
