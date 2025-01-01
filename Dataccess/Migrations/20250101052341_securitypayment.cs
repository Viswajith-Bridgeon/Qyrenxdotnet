using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qyrenx.Dataccess.Migrations
{
    /// <inheritdoc />
    public partial class securitypayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPayment_Status_StatusId",
                table: "UserPayment");

            migrationBuilder.DropColumn(
                name: "OriginalAmount",
                table: "UserPayment");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "UserPayment",
                newName: "AddressesId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPayment_StatusId",
                table: "UserPayment",
                newName: "IX_UserPayment_AddressesId");

            migrationBuilder.AlterColumn<decimal>(
                name: "SecurityAmount",
                table: "UserPayment",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AddColumn<string>(
                name: "PaymentString",
                table: "UserPayment",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "UserPayment",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "Gadgets",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Gadgets_TransactionId",
                table: "Gadgets",
                column: "TransactionId");

            migrationBuilder.CreateTable(
                name: "OrderGadget",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PaymentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    GadgetId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderGadget", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderGadget_UserPayment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "UserPayment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1f5d5da-e94d-44f1-a8c3-b60f42101a01"),
                columns: new[] { "CreatedOn", "HashPassword" },
                values: new object[] { new DateTime(2025, 1, 1, 10, 53, 38, 622, DateTimeKind.Local).AddTicks(4740), "$2a$11$7kQo7lPCTUR7XjR/2nw3xOnBl2hge19uIZbObH9WleXd51NLUsOOO" });

            migrationBuilder.CreateIndex(
                name: "IX_UserPayment_TransactionId",
                table: "UserPayment",
                column: "TransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderGadget_PaymentId",
                table: "OrderGadget",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gadgets_OrderGadget_Id",
                table: "Gadgets",
                column: "Id",
                principalTable: "OrderGadget",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPayment_Address_AddressesId",
                table: "UserPayment",
                column: "AddressesId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPayment_Gadgets_TransactionId",
                table: "UserPayment",
                column: "TransactionId",
                principalTable: "Gadgets",
                principalColumn: "TransactionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gadgets_OrderGadget_Id",
                table: "Gadgets");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPayment_Address_AddressesId",
                table: "UserPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPayment_Gadgets_TransactionId",
                table: "UserPayment");

            migrationBuilder.DropTable(
                name: "OrderGadget");

            migrationBuilder.DropIndex(
                name: "IX_UserPayment_TransactionId",
                table: "UserPayment");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Gadgets_TransactionId",
                table: "Gadgets");

            migrationBuilder.DropColumn(
                name: "PaymentString",
                table: "UserPayment");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "UserPayment");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Gadgets");

            migrationBuilder.RenameColumn(
                name: "AddressesId",
                table: "UserPayment",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPayment_AddressesId",
                table: "UserPayment",
                newName: "IX_UserPayment_StatusId");

            migrationBuilder.AlterColumn<decimal>(
                name: "SecurityAmount",
                table: "UserPayment",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AddColumn<decimal>(
                name: "OriginalAmount",
                table: "UserPayment",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1f5d5da-e94d-44f1-a8c3-b60f42101a01"),
                columns: new[] { "CreatedOn", "HashPassword" },
                values: new object[] { new DateTime(2024, 12, 31, 15, 18, 41, 796, DateTimeKind.Local).AddTicks(1019), "$2a$11$buJevdBKMd33uoVD4zEVOeDzcz74cBIYPLjYdhfQOc/5k7rCPp9rq" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserPayment_Status_StatusId",
                table: "UserPayment",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
