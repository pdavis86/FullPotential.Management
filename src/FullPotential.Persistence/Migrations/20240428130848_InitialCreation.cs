using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FullPotential.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(32)", maxLength: 32, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(256)", maxLength: 256, nullable: false),
                    Token = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    TokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: true),
                    IsEmailAddressValidated = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmailAddress",
                table: "Users",
                column: "EmailAddress",
                unique: true,
                filter: "[IsEmailAddressValidated] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
