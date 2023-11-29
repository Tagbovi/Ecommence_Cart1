using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ecomence_Cart.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "CarT",
                columns: table => new
                {
                    ItemID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemName = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<double>(nullable: false),
                    phoneNumbers = table.Column<int>(nullable: false),
                    AddedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarT", x => x.ItemID);
                });

            migrationBuilder.InsertData(
                table: "CarT",
                columns: new[] { "ItemID", "AddedTime", "ItemName", "Quantity", "UnitPrice", "phoneNumbers" },
                values: new object[] { 1, new DateTime(2023, 11, 16, 14, 20, 29, 334, DateTimeKind.Utc).AddTicks(553), "Air-Max", 1, 1000.0, 20303346 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarT");
        }
    }
}
