using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RevisionTwoApp.RestApi.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressLine1 = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    City = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BillDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Branch = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    InventoryID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    TransactionDescription = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Qty = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    UnitCost = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Account = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    CostCode = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    POOrderNbr = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ReferenceNbr = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PostPeriod = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Vendor = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    VendorRef = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    CurrencyID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BusinessAccount = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BusinessAccountName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Severity = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Priority = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Owner = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Workgroup = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ClassID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DateReported = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastActivityDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ContactID = table.Column<int>(type: "int", maxLength: 128, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ContactClass = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BusinessAccount = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Owner = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Credentials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsChecked = table.Column<bool>(type: "bit", nullable: false),
                    SiteUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Tenant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Locale = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credentials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Opportunities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpportunityID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Stage = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Estimation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CurrencyID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ClassID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Owner = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ContactID = table.Column<int>(type: "int", maxLength: 128, nullable: true),
                    ContactDisplayName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    BusinessAccount = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opportunities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    OrderNbr = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    OrderedQty = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    OrderTotal = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    CurrencyID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ShipmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderType = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    OrderNbr = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    InventoryID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    WarehouseID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ShippedQty = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    OrderedQty = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ShipmentNbr = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ShipmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    WarehouseID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ShippedQty = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    ShippedWeight = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "BillDetails");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Credentials");

            migrationBuilder.DropTable(
                name: "Opportunities");

            migrationBuilder.DropTable(
                name: "SalesOrders");

            migrationBuilder.DropTable(
                name: "ShipmentDetails");

            migrationBuilder.DropTable(
                name: "Shipments");
        }
    }
}
