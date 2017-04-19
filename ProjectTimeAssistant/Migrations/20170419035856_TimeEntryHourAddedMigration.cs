using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTimeAssistant.Migrations
{
    public partial class TimeEntryHourAddedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishTime",
                table: "WorkTimes");

            migrationBuilder.AddColumn<double>(
                name: "Hours",
                table: "WorkTimes",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hours",
                table: "WorkTimes");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishTime",
                table: "WorkTimes",
                nullable: true);
        }
    }
}
