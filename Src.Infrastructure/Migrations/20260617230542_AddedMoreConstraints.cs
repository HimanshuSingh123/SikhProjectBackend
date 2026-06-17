using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedMoreConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Favourites_username",
                table: "Favourites");

            migrationBuilder.DropIndex(
                name: "IX_Cart_username",
                table: "Cart");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Cart",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "card_id",
                table: "Cart",
                newName: "cart_id");

            migrationBuilder.AddColumn<int>(
                name: "submission_id",
                table: "Purchase_History",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "submission_id",
                table: "Favourites",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "submission_id",
                table: "Cart",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_History_submission_id",
                table: "Purchase_History",
                column: "submission_id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_History_Merch_submission_id",
                table: "Purchase_History",
                column: "submission_id",
                principalTable: "Merch",
                principalColumn: "submission_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Merch_submission_id",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_Merch_submission_id",
                table: "Favourites");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_History_Merch_submission_id",
                table: "Purchase_History");

            migrationBuilder.DropIndex(
                name: "IX_Purchase_History_submission_id",
                table: "Purchase_History");

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

            migrationBuilder.DropColumn(
                name: "submission_id",
                table: "Purchase_History");

            migrationBuilder.DropColumn(
                name: "submission_id",
                table: "Favourites");

            migrationBuilder.DropColumn(
                name: "submission_id",
                table: "Cart");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Cart",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "cart_id",
                table: "Cart",
                newName: "card_id");

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_username",
                table: "Favourites",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_username",
                table: "Cart",
                column: "username");
        }
    }
}
