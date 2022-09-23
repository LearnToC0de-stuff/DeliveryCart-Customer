using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryCart_Customer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    customerID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    customerName = table.Column<string>(type: "TEXT", nullable: true),
                    customerEmail = table.Column<string>(type: "TEXT", nullable: true),
                    customerAddress = table.Column<string>(type: "TEXT", nullable: true),
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
                    deliveryDriverName = table.Column<string>(type: "TEXT", nullable: true),
                    deliveryDriverPhoneNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    orderID = table.Column<int>(type: "INTEGER", nullable: false)
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
                    customerID = table.Column<string>(type: "TEXT", nullable: true),
                    customerID1 = table.Column<int>(type: "INTEGER", nullable: true),
                    deliveryDriverID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.orderID);
                    table.ForeignKey(
                        name: "FK_Order_Customer_customerID1",
                        column: x => x.customerID1,
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
                name: "IX_Order_customerID1",
                table: "Order",
                column: "customerID1");

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
