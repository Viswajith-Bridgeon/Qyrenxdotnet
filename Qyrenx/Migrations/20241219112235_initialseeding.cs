using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qyrenx.Migrations
{
    /// <inheritdoc />
    public partial class initialseeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Date", "Email", "HashPassword", "IsBlock", "Mobile", "Name", "Role" },
                values: new object[] { new Guid("a1f5d5da-e94d-44f1-a8c3-b60f42101a01"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", "$2a$11$1C3XbsCsaNIqgKvomzDPHevpAL57sOK96.TjsmiP5FvK7CGbfr9zK", false, 1234567890, "Admin", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1f5d5da-e94d-44f1-a8c3-b60f42101a01"));
        }
    }
}
