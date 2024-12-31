using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qyrenx.Dataccess.Migrations
{
    /// <inheritdoc />
    public partial class address2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Gadgets",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1f5d5da-e94d-44f1-a8c3-b60f42101a01"),
                columns: new[] { "CreatedOn", "HashPassword" },
                values: new object[] { new DateTime(2024, 12, 31, 15, 18, 41, 796, DateTimeKind.Local).AddTicks(1019), "$2a$11$buJevdBKMd33uoVD4zEVOeDzcz74cBIYPLjYdhfQOc/5k7rCPp9rq" });

            migrationBuilder.CreateIndex(
                name: "IX_Gadgets_AddressId",
                table: "Gadgets",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gadgets_Address_AddressId",
                table: "Gadgets",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gadgets_Address_AddressId",
                table: "Gadgets");

            migrationBuilder.DropIndex(
                name: "IX_Gadgets_AddressId",
                table: "Gadgets");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Gadgets");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1f5d5da-e94d-44f1-a8c3-b60f42101a01"),
                columns: new[] { "CreatedOn", "HashPassword" },
                values: new object[] { new DateTime(2024, 12, 31, 10, 52, 57, 614, DateTimeKind.Local).AddTicks(2291), "$2a$11$5Swl/pCcjwZgvaHXzQ3kgekN9O8wICYdr.rcdWhta4ba.pULymf8q" });
        }
    }
}
