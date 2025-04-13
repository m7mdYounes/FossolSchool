using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FosoolSchool.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Levels_LevelId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Subjects_SubjectId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_LevelId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_SubjectId",
                table: "Classes");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d9948c0f-76a6-48cf-a76d-13907b54d389");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Classes");

            migrationBuilder.CreateTable(
                name: "LessonResources",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LessonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UploadedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonResources_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonResources_Users_UploadedById",
                        column: x => x.UploadedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherLessonResourceViews",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    TeacherId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ResourceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherLessonResourceViews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherLessonResourceViews_LessonResources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "LessonResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherLessonResourceViews_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedUserId", "IsDeleted", "Password", "UpdatedAt", "UpdatedUserId", "UserEmail", "UserName", "UserRole" },
                values: new object[] { "55f85ab4-5473-4cab-b0f5-7fe6efc54e4d", new DateTime(2025, 4, 13, 14, 6, 47, 156, DateTimeKind.Utc).AddTicks(8131), "null", false, "AQAAALDzk2IeAfQWn6Qn8lyhN4aRyfBrw5v34ev473NIjBBwkwTf+FuzYbuJMje5nemQcw==", new DateTime(2025, 4, 13, 14, 6, 47, 156, DateTimeKind.Utc).AddTicks(8133), "null", "admin@example.com", "admin", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_LessonResources_LessonId",
                table: "LessonResources",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonResources_UploadedById",
                table: "LessonResources",
                column: "UploadedById");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLessonResourceViews_ResourceId",
                table: "TeacherLessonResourceViews",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLessonResourceViews_TeacherId",
                table: "TeacherLessonResourceViews",
                column: "TeacherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeacherLessonResourceViews");

            migrationBuilder.DropTable(
                name: "LessonResources");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "55f85ab4-5473-4cab-b0f5-7fe6efc54e4d");

            migrationBuilder.AddColumn<string>(
                name: "LevelId",
                table: "Classes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubjectId",
                table: "Classes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedUserId", "IsDeleted", "Password", "UpdatedAt", "UpdatedUserId", "UserEmail", "UserName", "UserRole" },
                values: new object[] { "d9948c0f-76a6-48cf-a76d-13907b54d389", new DateTime(2025, 4, 12, 15, 50, 57, 37, DateTimeKind.Utc).AddTicks(3518), "null", false, "AQAAALDzk2IeAfQWn6Qn8lyhN4aRyfBrw5v34ev473NIjBBwkwTf+FuzYbuJMje5nemQcw==", new DateTime(2025, 4, 12, 15, 50, 57, 37, DateTimeKind.Utc).AddTicks(3520), "null", "admin@example.com", "admin", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_LevelId",
                table: "Classes",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_SubjectId",
                table: "Classes",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Levels_LevelId",
                table: "Classes",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Subjects_SubjectId",
                table: "Classes",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
