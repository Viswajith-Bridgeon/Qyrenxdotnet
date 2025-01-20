using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qyrenx.Dataccess.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AccountsVendorDeliveries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PersonId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IFSC = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountNo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsVendorDeliveries", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CategoryName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CategoryDescription = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Image = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DeliveryPersonPayment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DeliveryPersonId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SalaryStatus = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    BonusStatus = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryPersonPayment", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DeliveryPersons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PickupId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DrivingLicense = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Mobile = table.Column<int>(type: "int", nullable: false),
                    HashPassword = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsBlock = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsVerified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryPersons", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PaymentToUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Userid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StatusId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Pay = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentToUsers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Mobile = table.Column<int>(type: "int", nullable: false),
                    HashPassword = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsBlock = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Mobile = table.Column<int>(type: "int", nullable: false),
                    ShopeName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShopeLicense = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OpeningTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ClosingTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    HashPassword = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsBlock = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsVerified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DeliveryPersonOnlines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DeliveryPersonId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Lat = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Long = table.Column<decimal>(type: "decimal(65,30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryPersonOnlines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryPersonOnlines_DeliveryPersons_DeliveryPersonId",
                        column: x => x.DeliveryPersonId,
                        principalTable: "DeliveryPersons",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    House = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LandMark = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PostalCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserPayment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SecurityAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentString = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TransactionId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPayment_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VendorAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    VendorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    House = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LandMark = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PostalCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorAddresses_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VendorCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    VendorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CategoryId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendorCategories_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Gadgets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CategoryId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    GadgetName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Image = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gadgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gadgets_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Gadgets_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gadgets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrderGadgets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PaymentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    GadgetId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderGadgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderGadgets_Gadgets_GadgetId",
                        column: x => x.GadgetId,
                        principalTable: "Gadgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderGadgets_UserPayment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "UserPayment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pickups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    GadgetId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    VendorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DeliveryPersonId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pickups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pickups_DeliveryPersons_DeliveryPersonId",
                        column: x => x.DeliveryPersonId,
                        principalTable: "DeliveryPersons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pickups_Gadgets_GadgetId",
                        column: x => x.GadgetId,
                        principalTable: "Gadgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pickups_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PickupId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Statuss = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Status_Pickups_PickupId",
                        column: x => x.PickupId,
                        principalTable: "Pickups",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VendorCost",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PickupId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    VendorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProblemDescription = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsServiceable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ServiceCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SaleCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    VendorsCostId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorCost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorCost_Pickups_PickupId",
                        column: x => x.PickupId,
                        principalTable: "Pickups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendorCost_VendorCost_VendorsCostId",
                        column: x => x.VendorsCostId,
                        principalTable: "VendorCost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendorCost_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VendorPayment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    VendorCostId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Payment = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorPayment_VendorCost_VendorCostId",
                        column: x => x.VendorCostId,
                        principalTable: "VendorCost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "Email", "HashPassword", "IsBlock", "IsDelete", "Mobile", "Name", "Role", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("a1f5d5da-e94d-44f1-a8c3-b60f42101a01"), null, new DateTime(2025, 1, 2, 11, 4, 2, 560, DateTimeKind.Local).AddTicks(3966), null, "admin@gmail.com", "$2a$11$q5wBlJLWVy6JVydzy4dCpuA6ga6vgxnZe7MA7di6q7UgseAjuWwnG", false, false, 1234567890, "Admin", "Admin", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserId",
                table: "Address",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryPersonOnlines_DeliveryPersonId",
                table: "DeliveryPersonOnlines",
                column: "DeliveryPersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gadgets_AddressId",
                table: "Gadgets",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Gadgets_CategoryId",
                table: "Gadgets",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Gadgets_UserId",
                table: "Gadgets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderGadgets_GadgetId",
                table: "OrderGadgets",
                column: "GadgetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderGadgets_PaymentId",
                table: "OrderGadgets",
                column: "PaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pickups_DeliveryPersonId",
                table: "Pickups",
                column: "DeliveryPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Pickups_GadgetId",
                table: "Pickups",
                column: "GadgetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pickups_VendorId",
                table: "Pickups",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Status_PickupId",
                table: "Status",
                column: "PickupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPayment_UserId",
                table: "UserPayment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorAddresses_VendorId",
                table: "VendorAddresses",
                column: "VendorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VendorCategories_CategoryId",
                table: "VendorCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorCategories_VendorId",
                table: "VendorCategories",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorCost_PickupId",
                table: "VendorCost",
                column: "PickupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VendorCost_VendorId",
                table: "VendorCost",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorCost_VendorsCostId",
                table: "VendorCost",
                column: "VendorsCostId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorPayment_VendorCostId",
                table: "VendorPayment",
                column: "VendorCostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountsVendorDeliveries");

            migrationBuilder.DropTable(
                name: "DeliveryPersonOnlines");

            migrationBuilder.DropTable(
                name: "DeliveryPersonPayment");

            migrationBuilder.DropTable(
                name: "OrderGadgets");

            migrationBuilder.DropTable(
                name: "PaymentToUsers");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "VendorAddresses");

            migrationBuilder.DropTable(
                name: "VendorCategories");

            migrationBuilder.DropTable(
                name: "VendorPayment");

            migrationBuilder.DropTable(
                name: "UserPayment");

            migrationBuilder.DropTable(
                name: "VendorCost");

            migrationBuilder.DropTable(
                name: "Pickups");

            migrationBuilder.DropTable(
                name: "DeliveryPersons");

            migrationBuilder.DropTable(
                name: "Gadgets");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
