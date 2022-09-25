using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstrutoraViverSA.Infrastructure.Migrations
{
    public partial class AtualizarTabelaEstoqueCampoOperacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EntradaSaidaEnum",
                table: "Estoque",
                newName: "Operacao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Operacao",
                table: "Estoque",
                newName: "EntradaSaidaEnum");
        }
    }
}
