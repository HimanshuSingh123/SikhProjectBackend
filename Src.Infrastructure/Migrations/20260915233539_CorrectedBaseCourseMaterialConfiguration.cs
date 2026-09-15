using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedBaseCourseMaterialConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VideoMaterial",
                table: "Writing_Lesson_Material",
                newName: "uploaded_material");

            migrationBuilder.RenameColumn(
                name: "VideoMaterial",
                table: "Speaking_Lesson_Material",
                newName: "uploaded_material");

            migrationBuilder.RenameColumn(
                name: "VideoMaterial",
                table: "Reading_Lesson_Material",
                newName: "uploaded_material");

            migrationBuilder.RenameColumn(
                name: "VideoMaterial",
                table: "Introduction_Material",
                newName: "uploaded_material");

            migrationBuilder.RenameColumn(
                name: "VideoMaterial",
                table: "Conclusion_Material",
                newName: "uploaded_material");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "uploaded_material",
                table: "Writing_Lesson_Material",
                newName: "VideoMaterial");

            migrationBuilder.RenameColumn(
                name: "uploaded_material",
                table: "Speaking_Lesson_Material",
                newName: "VideoMaterial");

            migrationBuilder.RenameColumn(
                name: "uploaded_material",
                table: "Reading_Lesson_Material",
                newName: "VideoMaterial");

            migrationBuilder.RenameColumn(
                name: "uploaded_material",
                table: "Introduction_Material",
                newName: "VideoMaterial");

            migrationBuilder.RenameColumn(
                name: "uploaded_material",
                table: "Conclusion_Material",
                newName: "VideoMaterial");
        }
    }
}
