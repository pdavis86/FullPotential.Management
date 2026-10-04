using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FullPotential.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Increasedstringfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                type: "varbinary(512)",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ItemProperties",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ItemAttributes",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "CharacterSettings",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                type: "varbinary(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(512)",
                oldMaxLength: 512);

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ItemProperties",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048);

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ItemAttributes",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048);

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "CharacterSettings",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048);
        }
    }
}
