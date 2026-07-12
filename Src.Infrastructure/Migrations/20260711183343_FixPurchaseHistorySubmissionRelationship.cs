using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixPurchaseHistorySubmissionRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Merch_MerchSubmissionId",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_Merch_MerchSubmissionId",
                table: "Favourites");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_History_Merch_submission_id",
                table: "Purchase_History");

            migrationBuilder.DropIndex(
                name: "IX_Favourites_MerchSubmissionId",
                table: "Favourites");

            migrationBuilder.DropIndex(
                name: "IX_Cart_MerchSubmissionId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "MerchSubmissionId",
                table: "Favourites");

            migrationBuilder.DropColumn(
                name: "MerchSubmissionId",
                table: "Cart");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_History_Submission_submission_id",
                table: "Purchase_History",
                column: "submission_id",
                principalTable: "Submission",
                principalColumn: "submission_id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_History_Submission_submission_id",
                table: "Purchase_History");

            migrationBuilder.AddColumn<int>(
                name: "MerchSubmissionId",
                table: "Favourites",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MerchSubmissionId",
                table: "Cart",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_MerchSubmissionId",
                table: "Favourites",
                column: "MerchSubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_MerchSubmissionId",
                table: "Cart",
                column: "MerchSubmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Merch_MerchSubmissionId",
                table: "Cart",
                column: "MerchSubmissionId",
                principalTable: "Merch",
                principalColumn: "submission_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_Merch_MerchSubmissionId",
                table: "Favourites",
                column: "MerchSubmissionId",
                principalTable: "Merch",
                principalColumn: "submission_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_History_Merch_submission_id",
                table: "Purchase_History",
                column: "submission_id",
                principalTable: "Merch",
                principalColumn: "submission_id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
