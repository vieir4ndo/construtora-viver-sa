using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstrutoraViverSA.Infrastructure.Migrations
{
    public partial class UpdateDatabaseFKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObraFuncionarios");

            migrationBuilder.DropTable(
                name: "ObraMateriais");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "Estoque",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "FuncionarioObra",
                columns: table => new
                {
                    FuncionariosId = table.Column<long>(type: "bigint", nullable: false),
                    ObrasId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncionarioObra", x => new { x.FuncionariosId, x.ObrasId });
                    table.ForeignKey(
                        name: "FK_FuncionarioObra_Funcionarios_FuncionariosId",
                        column: x => x.FuncionariosId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FuncionarioObra_Obras_ObrasId",
                        column: x => x.ObrasId,
                        principalTable: "Obras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialObra",
                columns: table => new
                {
                    MaterialsId = table.Column<long>(type: "bigint", nullable: false),
                    ObrasId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialObra", x => new { x.MaterialsId, x.ObrasId });
                    table.ForeignKey(
                        name: "FK_MaterialObra_Materiais_MaterialsId",
                        column: x => x.MaterialsId,
                        principalTable: "Materiais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialObra_Obras_ObrasId",
                        column: x => x.ObrasId,
                        principalTable: "Obras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioObra_ObrasId",
                table: "FuncionarioObra",
                column: "ObrasId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialObra_ObrasId",
                table: "MaterialObra",
                column: "ObrasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuncionarioObra");

            migrationBuilder.DropTable(
                name: "MaterialObra");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Estoque");

            migrationBuilder.CreateTable(
                name: "ObraFuncionarios",
                columns: table => new
                {
                    FuncionarioId = table.Column<long>(type: "bigint", nullable: false),
                    ObraId = table.Column<long>(type: "bigint", nullable: false)
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
                    MaterialId = table.Column<long>(type: "bigint", nullable: false),
                    ObraId = table.Column<long>(type: "bigint", nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false)
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
    }
}
