using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeMerchFieldsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "rating",
                table: "Merch",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<byte[]>(
                name: "image",
                table: "Merch",
                type: "bytea",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "bytea");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "rating",
                table: "Merch",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "image",
                table: "Merch",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldNullable: true);
        }
    }
}
