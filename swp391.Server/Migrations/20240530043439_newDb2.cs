using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHealthcareSystem.Migrations
{
    /// <inheritdoc />
    public partial class newDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "ServiceServiceOrder");

            migrationBuilder.CreateTable(
                name: "ServiceOrderDetails",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    ServiceOrderId = table.Column<string>(type: "char(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceOrderDetails", x => new { x.ServiceId, x.ServiceOrderId });
                    table.ForeignKey(
                        name: "FK_ServiceOrderDetails_ServiceOrders_ServiceOrderId",
                        column: x => x.ServiceOrderId,
                        principalTable: "ServiceOrders",
                        principalColumn: "ServiceOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceOrderDetails_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOrderDetails_ServiceOrderId",
                table: "ServiceOrderDetails",
                column: "ServiceOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceOrderDetails");

            migrationBuilder.CreateTable(
                name: "ServiceServiceOrder",
                columns: table => new
                {
                    ServiceOrdersServiceOrderId = table.Column<string>(type: "char(11)", nullable: false),
                    ServicesServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceServiceOrder", x => new { x.ServiceOrdersServiceOrderId, x.ServicesServiceId });
                    table.ForeignKey(
                        name: "FK_ServiceServiceOrder_ServiceOrders_ServiceOrdersServiceOrderId",
                        column: x => x.ServiceOrdersServiceOrderId,
                        principalTable: "ServiceOrders",
                        principalColumn: "ServiceOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceServiceOrder_Services_ServicesServiceId",
                        column: x => x.ServicesServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceServiceOrder_ServicesServiceId",
                table: "ServiceServiceOrder",
                column: "ServicesServiceId");
        }
    }
}
