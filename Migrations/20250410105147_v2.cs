using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FosoolSchool.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levels_AcademicTerms_AcademicTermId",
                table: "Levels");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjects_TeacherTerms_TeacherTermId",
                table: "TeacherSubjects");

            migrationBuilder.DropTable(
                name: "TeacherGrade");

            migrationBuilder.DropIndex(
                name: "IX_Levels_AcademicTermId",
                table: "Levels");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "1ecd6abe-285b-4c5d-a501-67ee1f2da27e");

            migrationBuilder.DropColumn(
                name: "AcademicTermId",
                table: "Levels");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherTermId",
                table: "TeacherSubjects",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "TeacherId",
                table: "TeacherSubjects",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "AcademicTerms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "AcademicTerms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedUserId", "IsDeleted", "Password", "UpdatedAt", "UpdatedUserId", "UserEmail", "UserName", "UserRole" },
                values: new object[] { "8cfcb250-4f9f-4460-bcab-860a4f83d0d7", new DateTime(2025, 4, 10, 10, 51, 44, 621, DateTimeKind.Utc).AddTicks(7914), "null", false, "AQAAALDzk2IeAfQWn6Qn8lyhN4aRyfBrw5v34ev473NIjBBwkwTf+FuzYbuJMje5nemQcw==", new DateTime(2025, 4, 10, 10, 51, 44, 621, DateTimeKind.Utc).AddTicks(7919), "null", "admin@example.com", "admin", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjects_TeacherId",
                table: "TeacherSubjects",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjects_TeacherTerms_TeacherTermId",
                table: "TeacherSubjects",
                column: "TeacherTermId",
                principalTable: "TeacherTerms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjects_Teachers_TeacherId",
                table: "TeacherSubjects",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjects_TeacherTerms_TeacherTermId",
                table: "TeacherSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjects_Teachers_TeacherId",
                table: "TeacherSubjects");

            migrationBuilder.DropIndex(
                name: "IX_TeacherSubjects_TeacherId",
                table: "TeacherSubjects");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8cfcb250-4f9f-4460-bcab-860a4f83d0d7");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "AcademicTerms");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "AcademicTerms");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherTermId",
                table: "TeacherSubjects",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AcademicTermId",
                table: "Levels",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TeacherGrade",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GradeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TeacherTermId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherGrade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherGrade_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherGrade_TeacherTerms_TeacherTermId",
                        column: x => x.TeacherTermId,
                        principalTable: "TeacherTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedUserId", "IsDeleted", "Password", "UpdatedAt", "UpdatedUserId", "UserEmail", "UserName", "UserRole" },
                values: new object[] { "1ecd6abe-285b-4c5d-a501-67ee1f2da27e", new DateTime(2025, 4, 9, 15, 54, 12, 936, DateTimeKind.Utc).AddTicks(2109), "null", false, "123456string", new DateTime(2025, 4, 9, 15, 54, 12, 936, DateTimeKind.Utc).AddTicks(2112), "null", "admin@example.com", "admin", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Levels_AcademicTermId",
                table: "Levels",
                column: "AcademicTermId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherGrade_GradeId",
                table: "TeacherGrade",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherGrade_TeacherTermId",
                table: "TeacherGrade",
                column: "TeacherTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_AcademicTerms_AcademicTermId",
                table: "Levels",
                column: "AcademicTermId",
                principalTable: "AcademicTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjects_TeacherTerms_TeacherTermId",
                table: "TeacherSubjects",
                column: "TeacherTermId",
                principalTable: "TeacherTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
