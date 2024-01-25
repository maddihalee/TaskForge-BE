using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskForge.Migrations
{
    /// <inheritdoc />
    public partial class duedate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Tasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Tasks",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                column: "DueDate",
                value: new DateTime(2024, 1, 17, 15, 52, 39, 381, DateTimeKind.Local).AddTicks(9752));
        }
    }
}
