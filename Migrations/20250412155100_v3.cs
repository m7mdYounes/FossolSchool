using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FosoolSchool.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Users_UserId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_UserId",
                table: "Teachers");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8cfcb250-4f9f-4460-bcab-860a4f83d0d7");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Teachers");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedUserId", "IsDeleted", "Password", "UpdatedAt", "UpdatedUserId", "UserEmail", "UserName", "UserRole" },
                values: new object[] { "d9948c0f-76a6-48cf-a76d-13907b54d389", new DateTime(2025, 4, 12, 15, 50, 57, 37, DateTimeKind.Utc).AddTicks(3518), "null", false, "AQAAALDzk2IeAfQWn6Qn8lyhN4aRyfBrw5v34ev473NIjBBwkwTf+FuzYbuJMje5nemQcw==", new DateTime(2025, 4, 12, 15, 50, 57, 37, DateTimeKind.Utc).AddTicks(3520), "null", "admin@example.com", "admin", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d9948c0f-76a6-48cf-a76d-13907b54d389");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Teachers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedUserId", "IsDeleted", "Password", "UpdatedAt", "UpdatedUserId", "UserEmail", "UserName", "UserRole" },
                values: new object[] { "8cfcb250-4f9f-4460-bcab-860a4f83d0d7", new DateTime(2025, 4, 10, 10, 51, 44, 621, DateTimeKind.Utc).AddTicks(7914), "null", false, "AQAAALDzk2IeAfQWn6Qn8lyhN4aRyfBrw5v34ev473NIjBBwkwTf+FuzYbuJMje5nemQcw==", new DateTime(2025, 4, 10, 10, 51, 44, 621, DateTimeKind.Utc).AddTicks(7919), "null", "admin@example.com", "admin", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_UserId",
                table: "Teachers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Users_UserId",
                table: "Teachers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
