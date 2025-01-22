using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qyrenx.Dataccess.Migrations
{
    /// <inheritdoc />
    public partial class vendorcost2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsServiceable",
                table: "VendorCost",
                newName: "IsVenorServiceable");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1f5d5da-e94d-44f1-a8c3-b60f42101a01"),
                columns: new[] { "CreatedOn", "HashPassword" },
                values: new object[] { new DateTime(2025, 1, 21, 10, 51, 21, 100, DateTimeKind.Utc).AddTicks(8469), "$2a$11$6njRW3jxU2qdaU9D1eWJXeTn5cYBPRANzmFneRcW3JfDhN9V5WlIm" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsVenorServiceable",
                table: "VendorCost",
                newName: "IsServiceable");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1f5d5da-e94d-44f1-a8c3-b60f42101a01"),
                columns: new[] { "CreatedOn", "HashPassword" },
                values: new object[] { new DateTime(2025, 1, 21, 4, 30, 42, 923, DateTimeKind.Utc).AddTicks(9580), "$2a$11$sngNkBlbfWqbKRQw/qqWSe7D5fDtJWO1w9zD2CFuMBNICb.q9Ywx6" });
        }
    }
}
