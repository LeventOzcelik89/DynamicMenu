using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamicMenu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MenuBaseItemAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_Menu_MenuId",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "IconPath",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "TextEn",
                table: "MenuItem");

            migrationBuilder.RenameColumn(
                name: "NewTag",
                table: "MenuItem",
                newName: "IsNew");

            migrationBuilder.RenameColumn(
                name: "menuTarget",
                table: "Menu",
                newName: "MenuTarget");

            migrationBuilder.AlterColumn<int>(
                name: "MenuId",
                table: "MenuItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Keyword",
                table: "MenuItem",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "MenuBaseItemId",
                table: "MenuItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MenuGroupId",
                table: "MenuItem",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "MenuBaseItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuBaseItem", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_MenuBaseItemId",
                table: "MenuItem",
                column: "MenuBaseItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_MenuGroupId",
                table: "MenuItem",
                column: "MenuGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_MenuBaseItem_MenuBaseItemId",
                table: "MenuItem",
                column: "MenuBaseItemId",
                principalTable: "MenuBaseItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_MenuGroup_MenuGroupId",
                table: "MenuItem",
                column: "MenuGroupId",
                principalTable: "MenuGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_Menu_MenuId",
                table: "MenuItem",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_MenuBaseItem_MenuBaseItemId",
                table: "MenuItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_MenuGroup_MenuGroupId",
                table: "MenuItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_Menu_MenuId",
                table: "MenuItem");

            migrationBuilder.DropTable(
                name: "MenuBaseItem");

            migrationBuilder.DropIndex(
                name: "IX_MenuItem_MenuBaseItemId",
                table: "MenuItem");

            migrationBuilder.DropIndex(
                name: "IX_MenuItem_MenuGroupId",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "MenuBaseItemId",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "MenuGroupId",
                table: "MenuItem");

            migrationBuilder.RenameColumn(
                name: "IsNew",
                table: "MenuItem",
                newName: "NewTag");

            migrationBuilder.RenameColumn(
                name: "MenuTarget",
                table: "Menu",
                newName: "menuTarget");

            migrationBuilder.AlterColumn<int>(
                name: "MenuId",
                table: "MenuItem",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Keyword",
                table: "MenuItem",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "IconPath",
                table: "MenuItem",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "MenuItem",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TextEn",
                table: "MenuItem",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_Menu_MenuId",
                table: "MenuItem",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
