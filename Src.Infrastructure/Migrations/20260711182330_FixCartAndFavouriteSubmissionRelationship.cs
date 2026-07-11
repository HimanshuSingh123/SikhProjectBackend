using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixCartAndFavouriteSubmissionRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Merch_submission_id",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_Merch_submission_id",
                table: "Favourites");

            migrationBuilder.DropIndex(
                name: "FavouritesUserSubmissionKey",
                table: "Favourites");

            migrationBuilder.DropIndex(
                name: "IX_Favourites_submission_id",
                table: "Favourites");

            migrationBuilder.DropIndex(
                name: "CartUserSubmissionKey",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_submission_id",
                table: "Cart");

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
                name: "FavouritesUserSubmissionKey",
                table: "Favourites",
                columns: new[] { "username", "submission_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_MerchSubmissionId",
                table: "Favourites",
                column: "MerchSubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_submission_id",
                table: "Favourites",
                column: "submission_id");

            migrationBuilder.CreateIndex(
                name: "CartUserSubmissionKey",
                table: "Cart",
                columns: new[] { "username", "submission_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_MerchSubmissionId",
                table: "Cart",
                column: "MerchSubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_submission_id",
                table: "Cart",
                column: "submission_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Merch_MerchSubmissionId",
                table: "Cart",
                column: "MerchSubmissionId",
                principalTable: "Merch",
                principalColumn: "submission_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Submission_submission_id",
                table: "Cart",
                column: "submission_id",
                principalTable: "Submission",
                principalColumn: "submission_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_Merch_MerchSubmissionId",
                table: "Favourites",
                column: "MerchSubmissionId",
                principalTable: "Merch",
                principalColumn: "submission_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_Submission_submission_id",
                table: "Favourites",
                column: "submission_id",
                principalTable: "Submission",
                principalColumn: "submission_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Merch_MerchSubmissionId",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Submission_submission_id",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_Merch_MerchSubmissionId",
                table: "Favourites");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_Submission_submission_id",
                table: "Favourites");

            migrationBuilder.DropIndex(
                name: "FavouritesUserSubmissionKey",
                table: "Favourites");

            migrationBuilder.DropIndex(
                name: "IX_Favourites_MerchSubmissionId",
                table: "Favourites");

            migrationBuilder.DropIndex(
                name: "IX_Favourites_submission_id",
                table: "Favourites");

            migrationBuilder.DropIndex(
                name: "CartUserSubmissionKey",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_MerchSubmissionId",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_submission_id",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "MerchSubmissionId",
                table: "Favourites");

            migrationBuilder.DropColumn(
                name: "MerchSubmissionId",
                table: "Cart");

            migrationBuilder.CreateIndex(
                name: "FavouritesUserSubmissionKey",
                table: "Favourites",
                columns: new[] { "username", "submission_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_submission_id",
                table: "Favourites",
                column: "submission_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "CartUserSubmissionKey",
                table: "Cart",
                columns: new[] { "username", "submission_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_submission_id",
                table: "Cart",
                column: "submission_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Merch_submission_id",
                table: "Cart",
                column: "submission_id",
                principalTable: "Merch",
                principalColumn: "submission_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_Merch_submission_id",
                table: "Favourites",
                column: "submission_id",
                principalTable: "Merch",
                principalColumn: "submission_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
