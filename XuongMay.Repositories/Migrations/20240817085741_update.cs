using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XuongMay.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategorys_CategoryID",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategorys",
                table: "ProductCategorys");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "OrderProductionLines");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "OrderProductionLines");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "OrderProductionLines");

            migrationBuilder.DropColumn(
                name: "DeletedTime",
                table: "OrderProductionLines");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderProductionLines");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "OrderProductionLines");

            migrationBuilder.DropColumn(
                name: "LastUpdatedTime",
                table: "OrderProductionLines");

            migrationBuilder.RenameTable(
                name: "ProductCategorys",
                newName: "Categorys");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categorys",
                table: "Categorys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categorys_CategoryID",
                table: "Products",
                column: "CategoryID",
                principalTable: "Categorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categorys_CategoryID",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categorys",
                table: "Categorys");

            migrationBuilder.RenameTable(
                name: "Categorys",
                newName: "ProductCategorys");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "OrderProductionLines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedTime",
                table: "OrderProductionLines",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "OrderProductionLines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedTime",
                table: "OrderProductionLines",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "OrderProductionLines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedBy",
                table: "OrderProductionLines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastUpdatedTime",
                table: "OrderProductionLines",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategorys",
                table: "ProductCategorys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategorys_CategoryID",
                table: "Products",
                column: "CategoryID",
                principalTable: "ProductCategorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
