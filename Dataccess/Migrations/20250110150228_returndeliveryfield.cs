using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qyrenx.Dataccess.Migrations
{
    /// <inheritdoc />
    public partial class returndeliveryfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReturnDeliveryPersonId",
                table: "Pickups",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1f5d5da-e94d-44f1-a8c3-b60f42101a01"),
                columns: new[] { "CreatedOn", "HashPassword" },
                values: new object[] { new DateTime(2025, 1, 10, 15, 2, 24, 361, DateTimeKind.Utc).AddTicks(8191), "$2a$11$MV2IiMtzPJC7007hvpC9zuivHPgStxXiL.dsdxXlgU.MzjIszg5nm" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnDeliveryPersonId",
                table: "Pickups");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1f5d5da-e94d-44f1-a8c3-b60f42101a01"),
                columns: new[] { "CreatedOn", "HashPassword" },
                values: new object[] { new DateTime(2025, 1, 9, 11, 46, 15, 341, DateTimeKind.Utc).AddTicks(802), "$2a$11$8B2xc3fEArvEYtXumqCsP.GQeIoQ3J/iRPO/jD1EmYulnZSBUIc4O" });
        }
    }
}
