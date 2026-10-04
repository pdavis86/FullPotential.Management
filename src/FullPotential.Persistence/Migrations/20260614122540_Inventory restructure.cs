using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FullPotential.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Inventoryrestructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Users_OwnerId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Characters_OwnerId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "CombatItemEffects");

            migrationBuilder.DropTable(
                name: "ItemDrawings");

            migrationBuilder.DropTable(
                name: "CombatItems");

            migrationBuilder.DropIndex(
                name: "IX_CharacterSettings_CharacterId",
                table: "CharacterSettings");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "VisualsTypeId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Items",
                newName: "CharacterId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_OwnerId",
                table: "Items",
                newName: "IX_Items_CharacterId");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Characters",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Characters_OwnerId",
                table: "Characters",
                newName: "IX_Characters_UserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "RegistryTypeId",
                table: "Items",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "SlotId",
                table: "CharacterEquippedItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "CharacterValuePools",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterValuePools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterValuePools_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemEffects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EffectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemEffects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemEffects_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemProperties",
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
                    table.PrimaryKey("PK_ItemProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemProperties_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSettings_CharacterId_Key",
                table: "CharacterSettings",
                columns: new[] { "CharacterId", "Key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CharacterValuePools_CharacterId",
                table: "CharacterValuePools",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemEffects_ItemId",
                table: "ItemEffects",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemProperties_ItemId",
                table: "ItemProperties",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Users_UserId",
                table: "Characters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Characters_CharacterId",
                table: "Items",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Users_UserId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Characters_CharacterId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "CharacterValuePools");

            migrationBuilder.DropTable(
                name: "ItemEffects");

            migrationBuilder.DropTable(
                name: "ItemProperties");

            migrationBuilder.DropIndex(
                name: "IX_CharacterSettings_CharacterId_Key",
                table: "CharacterSettings");

            migrationBuilder.RenameColumn(
                name: "CharacterId",
                table: "Items",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_CharacterId",
                table: "Items",
                newName: "IX_Items_OwnerId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Characters",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Characters_UserId",
                table: "Characters",
                newName: "IX_Characters_OwnerId");

            migrationBuilder.AlterColumn<Guid>(
                name: "RegistryTypeId",
                table: "Items",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VisualsTypeId",
                table: "Items",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SlotId",
                table: "CharacterEquippedItems",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "CombatItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ammo = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    IsTwoHanded = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShapeTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShapeVisualsTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TargetingTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TargetingVisualsTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ValuePoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                name: "ItemDrawings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DrawingCode = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
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
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    EffectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "IX_ItemDrawings_ItemId",
                table: "ItemDrawings",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Users_OwnerId",
                table: "Characters",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Characters_OwnerId",
                table: "Items",
                column: "OwnerId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
