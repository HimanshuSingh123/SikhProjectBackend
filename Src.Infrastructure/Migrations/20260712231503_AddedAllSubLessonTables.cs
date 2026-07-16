using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedAllSubLessonTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conclusion_Material",
                columns: table => new
                {
                    submission_id = table.Column<int>(type: "integer", nullable: false),
                    uploaded_material = table.Column<byte[]>(type: "bytea", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conclusion_Material", x => x.submission_id);
                    table.ForeignKey(
                        name: "FK_Conclusion_Material_Course_submission_id",
                        column: x => x.submission_id,
                        principalTable: "Course",
                        principalColumn: "submission_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Introduction_Material",
                columns: table => new
                {
                    submission_id = table.Column<int>(type: "integer", nullable: false),
                    uploaded_material = table.Column<byte[]>(type: "bytea", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Introduction_Material", x => x.submission_id);
                    table.ForeignKey(
                        name: "FK_Introduction_Material_Course_submission_id",
                        column: x => x.submission_id,
                        principalTable: "Course",
                        principalColumn: "submission_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reading_Lesson_Material",
                columns: table => new
                {
                    submission_id = table.Column<int>(type: "integer", nullable: false),
                    uploaded_material = table.Column<byte[]>(type: "bytea", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reading_Lesson_Material", x => x.submission_id);
                    table.ForeignKey(
                        name: "FK_Reading_Lesson_Material_Course_submission_id",
                        column: x => x.submission_id,
                        principalTable: "Course",
                        principalColumn: "submission_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Speaking_Lesson_Material",
                columns: table => new
                {
                    submission_id = table.Column<int>(type: "integer", nullable: false),
                    uploaded_material = table.Column<byte[]>(type: "bytea", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speaking_Lesson_Material", x => x.submission_id);
                    table.ForeignKey(
                        name: "FK_Speaking_Lesson_Material_Course_submission_id",
                        column: x => x.submission_id,
                        principalTable: "Course",
                        principalColumn: "submission_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Writing_Lesson_Material",
                columns: table => new
                {
                    submission_id = table.Column<int>(type: "integer", nullable: false),
                    uploaded_material = table.Column<byte[]>(type: "bytea", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Writing_Lesson_Material", x => x.submission_id);
                    table.ForeignKey(
                        name: "FK_Writing_Lesson_Material_Course_submission_id",
                        column: x => x.submission_id,
                        principalTable: "Course",
                        principalColumn: "submission_id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conclusion_Material");

            migrationBuilder.DropTable(
                name: "Introduction_Material");

            migrationBuilder.DropTable(
                name: "Reading_Lesson_Material");

            migrationBuilder.DropTable(
                name: "Speaking_Lesson_Material");

            migrationBuilder.DropTable(
                name: "Writing_Lesson_Material");
        }
    }
}
