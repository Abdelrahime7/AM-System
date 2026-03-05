using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameTokenToRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "used_at",
                table: "tokens",
                newName: "RevokedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "tokens",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRevoked",
                table: "tokens",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "tokens");

            migrationBuilder.DropColumn(
                name: "IsRevoked",
                table: "tokens");

            migrationBuilder.RenameColumn(
                name: "RevokedAt",
                table: "tokens",
                newName: "used_at");
        }
    }
}
