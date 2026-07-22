using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedRegisteredCourseAndExtaColumnToLessonTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "uploaded_material",
                table: "Writing_Lesson_Material",
                newName: "video_material");

            migrationBuilder.RenameColumn(
                name: "uploaded_material",
                table: "Speaking_Lesson_Material",
                newName: "video_material");

            migrationBuilder.RenameColumn(
                name: "uploaded_material",
                table: "Reading_Lesson_Material",
                newName: "video_material");

            migrationBuilder.RenameColumn(
                name: "uploaded_material",
                table: "Introduction_Material",
                newName: "video_material");

            migrationBuilder.RenameColumn(
                name: "uploaded_material",
                table: "Conclusion_Material",
                newName: "video_material");

            migrationBuilder.AddColumn<byte[]>(
                name: "VideoMaterial",
                table: "Writing_Lesson_Material",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "VideoMaterial",
                table: "Speaking_Lesson_Material",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "VideoMaterial",
                table: "Reading_Lesson_Material",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "VideoMaterial",
                table: "Introduction_Material",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "VideoMaterial",
                table: "Conclusion_Material",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateTable(
                name: "Registered_Courses",
                columns: table => new
                {
                    submission_id = table.Column<int>(type: "integer", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    registered_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registered_Courses", x => new { x.submission_id, x.username });
                    table.ForeignKey(
                        name: "FK_Registered_Courses_Course_submission_id",
                        column: x => x.submission_id,
                        principalTable: "Course",
                        principalColumn: "submission_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registered_Courses_User_username",
                        column: x => x.username,
                        principalTable: "User",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registered_Courses_username",
                table: "Registered_Courses",
                column: "username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registered_Courses");

            migrationBuilder.DropColumn(
                name: "VideoMaterial",
                table: "Writing_Lesson_Material");

            migrationBuilder.DropColumn(
                name: "VideoMaterial",
                table: "Speaking_Lesson_Material");

            migrationBuilder.DropColumn(
                name: "VideoMaterial",
                table: "Reading_Lesson_Material");

            migrationBuilder.DropColumn(
                name: "VideoMaterial",
                table: "Introduction_Material");

            migrationBuilder.DropColumn(
                name: "VideoMaterial",
                table: "Conclusion_Material");

            migrationBuilder.RenameColumn(
                name: "video_material",
                table: "Writing_Lesson_Material",
                newName: "uploaded_material");

            migrationBuilder.RenameColumn(
                name: "video_material",
                table: "Speaking_Lesson_Material",
                newName: "uploaded_material");

            migrationBuilder.RenameColumn(
                name: "video_material",
                table: "Reading_Lesson_Material",
                newName: "uploaded_material");

            migrationBuilder.RenameColumn(
                name: "video_material",
                table: "Introduction_Material",
                newName: "uploaded_material");

            migrationBuilder.RenameColumn(
                name: "video_material",
                table: "Conclusion_Material",
                newName: "uploaded_material");
        }
    }
}
