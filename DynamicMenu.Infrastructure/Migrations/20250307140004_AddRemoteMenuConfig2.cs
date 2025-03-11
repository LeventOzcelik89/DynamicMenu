using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamicMenu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRemoteMenuConfig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RemoteMenuConfig",
                table: "RemoteMenuConfig");

            migrationBuilder.RenameTable(
                name: "RemoteMenuConfig",
                newName: "RemoteMenus");

            migrationBuilder.AlterColumn<string>(
                name: "SubMenu",
                table: "RemoteMenus",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RemoteMenus",
                table: "RemoteMenus",
                column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RemoteMenus",
                table: "RemoteMenus");

            migrationBuilder.RenameTable(
                name: "RemoteMenus",
                newName: "RemoteMenuConfig");

            migrationBuilder.AlterColumn<string>(
                name: "SubMenu",
                table: "RemoteMenuConfig",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RemoteMenuConfig",
                table: "RemoteMenuConfig",
                column: "ID");
        }
    }
}
