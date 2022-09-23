using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstrutoraViverSA.Infraestrutura.Migrations
{
    public partial class UpdateDatabaseStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OrcamentoId",
                table: "Obras",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "ObraFuncionarios",
                columns: table => new
                {
                    ObraId = table.Column<long>(type: "bigint", nullable: false),
                    FuncionarioId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObraFuncionarios", x => new { x.FuncionarioId, x.ObraId });
                    table.ForeignKey(
                        name: "FK_ObraFuncionarios_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObraFuncionarios_Obras_ObraId",
                        column: x => x.ObraId,
                        principalTable: "Obras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObraMateriais",
                columns: table => new
                {
                    ObraId = table.Column<long>(type: "bigint", nullable: false),
                    MaterialId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObraMateriais", x => new { x.MaterialId, x.ObraId });
                    table.ForeignKey(
                        name: "FK_ObraMateriais_Materiais_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materiais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObraMateriais_Obras_ObraId",
                        column: x => x.ObraId,
                        principalTable: "Obras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ObraFuncionarios_ObraId",
                table: "ObraFuncionarios",
                column: "ObraId");

            migrationBuilder.CreateIndex(
                name: "IX_ObraMateriais_ObraId",
                table: "ObraMateriais",
                column: "ObraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObraFuncionarios");

            migrationBuilder.DropTable(
                name: "ObraMateriais");

            migrationBuilder.DropColumn(
                name: "OrcamentoId",
                table: "Obras");
        }
    }
}
