using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FullPotential.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Creation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterEquippedItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SlotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterEquippedItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterEquippedItems_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterSettings_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegistryTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    VisualsTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Characters_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CombatItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsTwoHanded = table.Column<bool>(type: "bit", nullable: false),
                    TargetingTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TargetingVisualsTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShapeTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShapeVisualsTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ValuePoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Ammo = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CombatItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemAttributes_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemDrawings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DrawingCode = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDrawings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemDrawings_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CombatItemEffects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CombatItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EffectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatItemEffects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CombatItemEffects_CombatItems_CombatItemId",
                        column: x => x.CombatItemId,
                        principalTable: "CombatItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterEquippedItems_CharacterId",
                table: "CharacterEquippedItems",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_OwnerId",
                table: "Characters",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSettings_CharacterId",
                table: "CharacterSettings",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CombatItemEffects_CombatItemId",
                table: "CombatItemEffects",
                column: "CombatItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CombatItems_ItemId",
                table: "CombatItems",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemAttributes_ItemId",
                table: "ItemAttributes",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDrawings_ItemId",
                table: "ItemDrawings",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_OwnerId",
                table: "Items",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmailAddress",
                table: "Users",
                column: "EmailAddress",
                unique: true,
                filter: "[IsEmailAddressValidated] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterEquippedItems");

            migrationBuilder.DropTable(
                name: "CharacterSettings");

            migrationBuilder.DropTable(
                name: "CombatItemEffects");

            migrationBuilder.DropTable(
                name: "Instances");

            migrationBuilder.DropTable(
                name: "ItemAttributes");

            migrationBuilder.DropTable(
                name: "ItemDrawings");

            migrationBuilder.DropTable(
                name: "CombatItems");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
