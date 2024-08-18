using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XuongMay.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Update_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "OrderTasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedTime",
                table: "OrderTasks",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "OrderTasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedTime",
                table: "OrderTasks",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "OrderTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedBy",
                table: "OrderTasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastUpdatedTime",
                table: "OrderTasks",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "OrderTasks");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "OrderTasks");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "OrderTasks");

            migrationBuilder.DropColumn(
                name: "DeletedTime",
                table: "OrderTasks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderTasks");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "OrderTasks");

            migrationBuilder.DropColumn(
                name: "LastUpdatedTime",
                table: "OrderTasks");
        }
    }
}
