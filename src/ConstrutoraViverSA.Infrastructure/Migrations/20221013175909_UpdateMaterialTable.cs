using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstrutoraViverSA.Infrastructure.Migrations
{
    public partial class UpdateMaterialTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Estoque",
                newName: "DataHora");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataHora",
                table: "Estoque",
                newName: "DateTime");
        }
    }
}
