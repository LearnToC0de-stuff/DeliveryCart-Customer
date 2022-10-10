using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryCart_Customer.Migrations
{
    public partial class AnnotationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    customerID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    customerName = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    customerEmail = table.Column<string>(type: "TEXT", nullable: false),
                    customerAddress = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    customerPhoneNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.customerID);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryDriver",
                columns: table => new
                {
                    deliveryDriverID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    deliveryDriverName = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    deliveryDriverPhoneNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryDriver", x => x.deliveryDriverID);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    itemID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    itemDescription = table.Column<string>(type: "TEXT", nullable: true),
                    itemName = table.Column<string>(type: "TEXT", nullable: true),
                    itemPrice = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.itemID);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    orderID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    orderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    customerID = table.Column<int>(type: "INTEGER", nullable: true),
                    deliveryDriverID = table.Column<int>(type: "INTEGER", nullable: true),
                    itemID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.orderID);
                    table.ForeignKey(
                        name: "FK_Order_Customer_customerID",
                        column: x => x.customerID,
                        principalTable: "Customer",
                        principalColumn: "customerID");
                    table.ForeignKey(
                        name: "FK_Order_DeliveryDriver_deliveryDriverID",
                        column: x => x.deliveryDriverID,
                        principalTable: "DeliveryDriver",
                        principalColumn: "deliveryDriverID");
                });

            migrationBuilder.CreateTable(
                name: "ItemOrder",
                columns: table => new
                {
                    OrdersorderID = table.Column<int>(type: "INTEGER", nullable: false),
                    orderItemsitemID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemOrder", x => new { x.OrdersorderID, x.orderItemsitemID });
                    table.ForeignKey(
                        name: "FK_ItemOrder_Item_orderItemsitemID",
                        column: x => x.orderItemsitemID,
                        principalTable: "Item",
                        principalColumn: "itemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemOrder_Order_OrdersorderID",
                        column: x => x.OrdersorderID,
                        principalTable: "Order",
                        principalColumn: "orderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrder_orderItemsitemID",
                table: "ItemOrder",
                column: "orderItemsitemID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_customerID",
                table: "Order",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_deliveryDriverID",
                table: "Order",
                column: "deliveryDriverID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemOrder");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "DeliveryDriver");
        }
    }
}
