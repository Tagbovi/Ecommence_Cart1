using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ecomence_Cart.Migrations
{
    public partial class AddUpdatedTimeToCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpDatedTime",
                table: "CarT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CarT",
                keyColumn: "ItemID",
                keyValue: 1,
                column: "AddedTime",
                value: new DateTime(2023, 11, 25, 19, 31, 28, 422, DateTimeKind.Utc).AddTicks(7975));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpDatedTime",
                table: "CarT");

            migrationBuilder.UpdateData(
                table: "CarT",
                keyColumn: "ItemID",
                keyValue: 1,
                column: "AddedTime",
                value: new DateTime(2023, 11, 16, 14, 20, 29, 334, DateTimeKind.Utc).AddTicks(553));
        }
    }
}
