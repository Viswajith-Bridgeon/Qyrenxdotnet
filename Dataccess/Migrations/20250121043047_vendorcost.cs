using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qyrenx.Dataccess.Migrations
{
    /// <inheritdoc />
    public partial class vendorcost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsServices",
                table: "VendorCost",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1f5d5da-e94d-44f1-a8c3-b60f42101a01"),
                columns: new[] { "CreatedOn", "HashPassword" },
                values: new object[] { new DateTime(2025, 1, 21, 4, 30, 42, 923, DateTimeKind.Utc).AddTicks(9580), "$2a$11$sngNkBlbfWqbKRQw/qqWSe7D5fDtJWO1w9zD2CFuMBNICb.q9Ywx6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsServices",
                table: "VendorCost");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1f5d5da-e94d-44f1-a8c3-b60f42101a01"),
                columns: new[] { "CreatedOn", "HashPassword" },
                values: new object[] { new DateTime(2025, 1, 10, 15, 2, 24, 361, DateTimeKind.Utc).AddTicks(8191), "$2a$11$MV2IiMtzPJC7007hvpC9zuivHPgStxXiL.dsdxXlgU.MzjIszg5nm" });
        }
    }
}
