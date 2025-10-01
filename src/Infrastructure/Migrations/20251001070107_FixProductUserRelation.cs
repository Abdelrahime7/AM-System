using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixProductUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_users_CreatedById",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_CreatedById",
                table: "products");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "products");

            migrationBuilder.CreateIndex(
                name: "IX_products_created_by_user_id",
                table: "products",
                column: "created_by_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_users_created_by_user_id",
                table: "products",
                column: "created_by_user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_users_created_by_user_id",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_created_by_user_id",
                table: "products");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_products_CreatedById",
                table: "products",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_products_users_CreatedById",
                table: "products",
                column: "CreatedById",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
