using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamicMenu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedMenuGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItemRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RemoteMenus",
                table: "RemoteMenus");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                table: "MenuItems");

            migrationBuilder.RenameTable(
                name: "RemoteMenus",
                newName: "RemoteMenuConfig");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Menus",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Menus",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<int>(
                name: "MenuGroupId",
                table: "Menus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte>(
                name: "menuTarget",
                table: "Menus",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "MenuItems",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RemoteMenuConfig",
                table: "RemoteMenuConfig",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "MenuGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MenuType = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuGroup", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Menus_MenuGroupId",
                table: "Menus",
                column: "MenuGroupId");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "MenuGroup");

            migrationBuilder.DropIndex(
                name: "IX_Menus_MenuGroupId",
                table: "Menus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RemoteMenuConfig",
                table: "RemoteMenuConfig");

            migrationBuilder.DropColumn(
                name: "MenuGroupId",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "menuTarget",
                table: "Menus");

            migrationBuilder.RenameTable(
                name: "RemoteMenuConfig",
                newName: "RemoteMenus");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Menus",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Menus",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "MenuItems",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "MenuItems",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                table: "MenuItems",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RemoteMenus",
                table: "RemoteMenus",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "MenuItemRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItemRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItemRoles_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemRoles_MenuItemId",
                table: "MenuItemRoles",
                column: "MenuItemId");
        }
    }
}
