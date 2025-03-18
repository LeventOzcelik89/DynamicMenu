using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamicMenu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedMenuGroupRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_MenuItems_Pid",
                table: "MenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Menus_MenuId",
                table: "MenuItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Menus",
                table: "Menus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItems",
                table: "MenuItems");

            migrationBuilder.RenameTable(
                name: "Menus",
                newName: "Menu");

            migrationBuilder.RenameTable(
                name: "MenuItems",
                newName: "MenuItem");

            migrationBuilder.RenameIndex(
                name: "IX_Menus_MenuGroupId",
                table: "Menu",
                newName: "IX_Menu_MenuGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItems_Pid",
                table: "MenuItem",
                newName: "IX_MenuItem_Pid");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItems_MenuId",
                table: "MenuItem",
                newName: "IX_MenuItem_MenuId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menu",
                table: "Menu",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItem",
                table: "MenuItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_MenuGroup_MenuGroupId",
                table: "Menu",
                column: "MenuGroupId",
                principalTable: "MenuGroup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_MenuItem_Pid",
                table: "MenuItem",
                column: "Pid",
                principalTable: "MenuItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_Menu_MenuId",
                table: "MenuItem",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_MenuGroup_MenuGroupId",
                table: "Menu");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_MenuItem_Pid",
                table: "MenuItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_Menu_MenuId",
                table: "MenuItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItem",
                table: "MenuItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Menu",
                table: "Menu");

            migrationBuilder.RenameTable(
                name: "MenuItem",
                newName: "MenuItems");

            migrationBuilder.RenameTable(
                name: "Menu",
                newName: "Menus");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItem_Pid",
                table: "MenuItems",
                newName: "IX_MenuItems_Pid");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItem_MenuId",
                table: "MenuItems",
                newName: "IX_MenuItems_MenuId");

            migrationBuilder.RenameIndex(
                name: "IX_Menu_MenuGroupId",
                table: "Menus",
                newName: "IX_Menus_MenuGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItems",
                table: "MenuItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menus",
                table: "Menus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_MenuItems_Pid",
                table: "MenuItems",
                column: "Pid",
                principalTable: "MenuItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Menus_MenuId",
                table: "MenuItems",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_MenuGroup_MenuGroupId",
                table: "Menus",
                column: "MenuGroupId",
                principalTable: "MenuGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
