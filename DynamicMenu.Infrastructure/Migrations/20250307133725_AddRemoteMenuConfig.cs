using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamicMenu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRemoteMenuConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RemoteMenuConfig",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubMenu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemEnText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemEnDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayType = table.Column<short>(type: "smallint", nullable: false),
                    PopUpMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PopUpMessageEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemType = table.Column<short>(type: "smallint", nullable: false),
                    ItemKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewTag = table.Column<short>(type: "smallint", nullable: false),
                    SpecialMobileRole = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemoteMenuConfig", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RemoteMenuConfig");
        }
    }
}
