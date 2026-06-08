using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account_Type",
                columns: table => new
                {
                    account_type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account_Type", x => x.account_type);
                });

            migrationBuilder.CreateTable(
                name: "Individual_Permissions",
                columns: table => new
                {
                    permission = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Individual_Permissions", x => x.permission);
                });

            migrationBuilder.CreateTable(
                name: "Prayers",
                columns: table => new
                {
                    PrayerName = table.Column<string>(type: "text", nullable: false),
                    prayer_content = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prayers", x => x.PrayerName);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "text", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    hashed_pass = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    account_type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.UniqueConstraint("AK_User_username", x => x.username);
                    table.ForeignKey(
                        name: "FK_User_Account_Type_account_type",
                        column: x => x.account_type,
                        principalTable: "Account_Type",
                        principalColumn: "account_type",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account_Permissions",
                columns: table => new
                {
                    account_type = table.Column<string>(type: "text", nullable: false),
                    permission = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account_Permissions", x => new { x.account_type, x.permission });
                    table.ForeignKey(
                        name: "FK_Account_Permissions_Account_Type_account_type",
                        column: x => x.account_type,
                        principalTable: "Account_Type",
                        principalColumn: "account_type",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Account_Permissions_Individual_Permissions_permission",
                        column: x => x.permission,
                        principalTable: "Individual_Permissions",
                        principalColumn: "permission",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    card_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "text", nullable: false),
                    item_title = table.Column<string>(type: "text", nullable: false),
                    item_description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    category = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.card_id);
                    table.ForeignKey(
                        name: "FK_Cart_User_username",
                        column: x => x.username,
                        principalTable: "User",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favourites",
                columns: table => new
                {
                    fav_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "text", nullable: false),
                    item_title = table.Column<string>(type: "text", nullable: false),
                    item_description = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    category = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourites", x => x.fav_ID);
                    table.ForeignKey(
                        name: "FK_Favourites_User_username",
                        column: x => x.username,
                        principalTable: "User",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Purchase_History",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "text", nullable: false),
                    item_title = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    item_type = table.Column<string>(type: "text", nullable: false),
                    purchase_timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase_History", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Purchase_History_User_username",
                        column: x => x.username,
                        principalTable: "User",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Submission",
                columns: table => new
                {
                    submission_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "text", nullable: false),
                    date_submitted = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    category = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submission", x => x.submission_id);
                    table.ForeignKey(
                        name: "FK_Submission_User_username",
                        column: x => x.username,
                        principalTable: "User",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    submission_id = table.Column<int>(type: "integer", nullable: false),
                    course_name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    image = table.Column<byte[]>(type: "bytea", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    uploaded_material = table.Column<byte[]>(type: "bytea", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.submission_id);
                    table.ForeignKey(
                        name: "FK_Course_Submission_submission_id",
                        column: x => x.submission_id,
                        principalTable: "Submission",
                        principalColumn: "submission_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Merch",
                columns: table => new
                {
                    submission_id = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    image = table.Column<byte[]>(type: "bytea", nullable: false),
                    size = table.Column<string>(type: "text", nullable: false),
                    qty_max = table.Column<int>(type: "integer", nullable: false),
                    qty_min = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    rating = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merch", x => x.submission_id);
                    table.ForeignKey(
                        name: "FK_Merch_Submission_submission_id",
                        column: x => x.submission_id,
                        principalTable: "Submission",
                        principalColumn: "submission_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsFeed",
                columns: table => new
                {
                    submission_id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    image = table.Column<byte[]>(type: "bytea", nullable: false),
                    alert = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsFeed", x => x.submission_id);
                    table.ForeignKey(
                        name: "FK_NewsFeed_Submission_submission_id",
                        column: x => x.submission_id,
                        principalTable: "Submission",
                        principalColumn: "submission_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sikh_Event",
                columns: table => new
                {
                    submission_id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    image = table.Column<byte[]>(type: "bytea", nullable: false),
                    location = table.Column<string>(type: "text", nullable: false),
                    contact_info = table.Column<string>(type: "text", nullable: false),
                    event_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sikh_Event", x => x.submission_id);
                    table.ForeignKey(
                        name: "FK_Sikh_Event_Submission_submission_id",
                        column: x => x.submission_id,
                        principalTable: "Submission",
                        principalColumn: "submission_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_Permissions_permission",
                table: "Account_Permissions",
                column: "permission");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_username",
                table: "Cart",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_username",
                table: "Favourites",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_History_username",
                table: "Purchase_History",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "IX_Submission_username",
                table: "Submission",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "IX_User_account_type",
                table: "User",
                column: "account_type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account_Permissions");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Favourites");

            migrationBuilder.DropTable(
                name: "Merch");

            migrationBuilder.DropTable(
                name: "NewsFeed");

            migrationBuilder.DropTable(
                name: "Prayers");

            migrationBuilder.DropTable(
                name: "Purchase_History");

            migrationBuilder.DropTable(
                name: "Sikh_Event");

            migrationBuilder.DropTable(
                name: "Individual_Permissions");

            migrationBuilder.DropTable(
                name: "Submission");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Account_Type");
        }
    }
}
