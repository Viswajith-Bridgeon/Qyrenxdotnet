using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qyrenx.Dataccess.Migrations
{
    /// <inheritdoc />
    public partial class vendorcost3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorCost_VendorCost_VendorsCostId",
                table: "VendorCost");

            migrationBuilder.DropIndex(
                name: "IX_VendorCost_VendorsCostId",
                table: "VendorCost");

            migrationBuilder.DropColumn(
                name: "VendorsCostId",
                table: "VendorCost");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1f5d5da-e94d-44f1-a8c3-b60f42101a01"),
                columns: new[] { "CreatedOn", "HashPassword" },
                values: new object[] { new DateTime(2025, 1, 21, 12, 57, 48, 455, DateTimeKind.Utc).AddTicks(4549), "$2a$11$iT3AZ4.D5QrAVOwZsWY18O.Q6WwjQrWDVUaynZXRuFH1DSq7EkiWG" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VendorsCostId",
                table: "VendorCost",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1f5d5da-e94d-44f1-a8c3-b60f42101a01"),
                columns: new[] { "CreatedOn", "HashPassword" },
                values: new object[] { new DateTime(2025, 1, 21, 10, 51, 21, 100, DateTimeKind.Utc).AddTicks(8469), "$2a$11$6njRW3jxU2qdaU9D1eWJXeTn5cYBPRANzmFneRcW3JfDhN9V5WlIm" });

            migrationBuilder.CreateIndex(
                name: "IX_VendorCost_VendorsCostId",
                table: "VendorCost",
                column: "VendorsCostId");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorCost_VendorCost_VendorsCostId",
                table: "VendorCost",
                column: "VendorsCostId",
                principalTable: "VendorCost",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
