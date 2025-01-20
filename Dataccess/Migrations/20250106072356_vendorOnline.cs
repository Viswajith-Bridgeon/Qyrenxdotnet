using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qyrenx.Dataccess.Migrations
{
    /// <inheritdoc />
    public partial class vendorOnline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "VendorOnline",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1f5d5da-e94d-44f1-a8c3-b60f42101a01"),
                columns: new[] { "CreatedOn", "HashPassword" },
                values: new object[] { new DateTime(2025, 1, 6, 7, 23, 55, 242, DateTimeKind.Utc).AddTicks(4256), "$2a$11$gPfStPOa0rKjmIOVTf6BROerbssIsIaAOkiCT7zRs2xG3Qstte2m." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IsActive",
                table: "VendorOnline",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1f5d5da-e94d-44f1-a8c3-b60f42101a01"),
                columns: new[] { "CreatedOn", "HashPassword" },
                values: new object[] { new DateTime(2025, 1, 6, 5, 38, 56, 683, DateTimeKind.Utc).AddTicks(1060), "$2a$11$8rZx8i4YmwoQwxe3Pwh4cuH1ZOwu6IMR/AAKTLtuzvbzVJBTOiMI2" });
        }
    }
}
