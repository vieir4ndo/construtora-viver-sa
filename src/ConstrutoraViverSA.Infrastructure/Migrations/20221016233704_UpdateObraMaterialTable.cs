using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstrutoraViverSA.Infrastructure.Migrations
{
    public partial class UpdateObraMaterialTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "ObraMaterial",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "ObraMaterial");
        }
    }
}
