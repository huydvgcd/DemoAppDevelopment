using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoAppDevelopment.Migrations
{
    public partial class changeQuantityType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders Detail_Book_BookId",
                table: "Orders Detail");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders Detail_Orders_OrderId",
                table: "Orders Detail");

            migrationBuilder.DropIndex(
                name: "IX_Orders Detail_OrderId",
                table: "Orders Detail");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Orders Detail",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "Orders Detail",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Total",
                table: "Orders Detail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Carts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Total",
                table: "Carts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Book",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "ProfilePicture",
                table: "Book",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders Detail",
                table: "Orders Detail",
                columns: new[] { "OrderId", "BookId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders Detail_Book_BookId",
                table: "Orders Detail",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders Detail_Orders_OrderId",
                table: "Orders Detail",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders Detail_Book_BookId",
                table: "Orders Detail");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders Detail_Orders_OrderId",
                table: "Orders Detail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders Detail",
                table: "Orders Detail");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Orders Detail");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Carts");

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "Orders Detail",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Orders Detail",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Quantity",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "ProfilePicture",
                table: "Book",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders Detail_OrderId",
                table: "Orders Detail",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders Detail_Book_BookId",
                table: "Orders Detail",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders Detail_Orders_OrderId",
                table: "Orders Detail",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
