using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeDriverIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_users_driver_id",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "driver_id",
                table: "orders",
                newName: "DriverId");

            migrationBuilder.RenameIndex(
                name: "IX_orders_driver_id",
                table: "orders",
                newName: "IX_orders_DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_users_DriverId",
                table: "orders",
                column: "DriverId",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_users_DriverId",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "DriverId",
                table: "orders",
                newName: "driver_id");

            migrationBuilder.RenameIndex(
                name: "IX_orders_DriverId",
                table: "orders",
                newName: "IX_orders_driver_id");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_users_driver_id",
                table: "orders",
                column: "driver_id",
                principalTable: "users",
                principalColumn: "id");
        }
    }
}
