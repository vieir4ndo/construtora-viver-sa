using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstrutoraViverSA.Infrastructure.Migrations
{
    public partial class AtualizarTabelaEstoque : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OperacaoEstoque",
                table: "Estoque",
                newName: "EntradaSaidaEnum");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EntradaSaidaEnum",
                table: "Estoque",
                newName: "OperacaoEstoque");
        }
    }
}
