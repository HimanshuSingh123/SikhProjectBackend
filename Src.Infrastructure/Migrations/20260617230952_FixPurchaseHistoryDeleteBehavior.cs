using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixPurchaseHistoryDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_History_Merch_submission_id",
                table: "Purchase_History");

            migrationBuilder.AlterColumn<int>(
                name: "submission_id",
                table: "Purchase_History",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_History_Merch_submission_id",
                table: "Purchase_History",
                column: "submission_id",
                principalTable: "Merch",
                principalColumn: "submission_id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_History_Merch_submission_id",
                table: "Purchase_History");

            migrationBuilder.AlterColumn<int>(
                name: "submission_id",
                table: "Purchase_History",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_History_Merch_submission_id",
                table: "Purchase_History",
                column: "submission_id",
                principalTable: "Merch",
                principalColumn: "submission_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
