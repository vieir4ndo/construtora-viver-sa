using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstrutoraViverSA.Infrastructure.Migrations
{
    public partial class UpdateObraMaterialTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ObraMaterial_MaterialId_ObraId",
                table: "ObraMaterial");

            migrationBuilder.DropColumn(
                name: "DataHora",
                table: "Estoque");

            migrationBuilder.RenameColumn(
                name: "Quantidade",
                table: "ObraMaterial",
                newName: "Operacao");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHora",
                table: "ObraMaterial",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_ObraMaterial_MaterialId",
                table: "ObraMaterial",
                column: "MaterialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ObraMaterial_MaterialId",
                table: "ObraMaterial");

            migrationBuilder.DropColumn(
                name: "DataHora",
                table: "ObraMaterial");

            migrationBuilder.RenameColumn(
                name: "Operacao",
                table: "ObraMaterial",
                newName: "Quantidade");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHora",
                table: "Estoque",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_ObraMaterial_MaterialId_ObraId",
                table: "ObraMaterial",
                columns: new[] { "MaterialId", "ObraId" },
                unique: true);
        }
    }
}
