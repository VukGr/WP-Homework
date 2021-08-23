using Microsoft.EntityFrameworkCore.Migrations;

namespace Back.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Networks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Networks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NetworkID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Channels_Networks_NetworkID",
                        column: x => x.NetworkID,
                        principalTable: "Networks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Channel = table.Column<int>(type: "int", maxLength: 256, nullable: false),
                    Hour = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Color = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ChannelID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Slots_Channels_ChannelID",
                        column: x => x.ChannelID,
                        principalTable: "Channels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Channels_NetworkID",
                table: "Channels",
                column: "NetworkID");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_ChannelID",
                table: "Slots",
                column: "ChannelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slots");

            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.DropTable(
                name: "Networks");
        }
    }
}
