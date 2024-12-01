using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Whisper.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_locations_id",
                table: "users");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_LocationId",
                table: "users",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_locations_LocationId",
                table: "users",
                column: "LocationId",
                principalTable: "locations",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_locations_LocationId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_LocationId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "users");

            migrationBuilder.AddForeignKey(
                name: "FK_users_locations_id",
                table: "users",
                column: "id",
                principalTable: "locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
