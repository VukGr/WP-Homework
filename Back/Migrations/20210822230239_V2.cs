using Microsoft.EntityFrameworkCore.Migrations;

namespace Back.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Channel",
                table: "Slots");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Channel",
                table: "Slots",
                type: "int",
                maxLength: 256,
                nullable: false,
                defaultValue: 0);
        }
    }
}
