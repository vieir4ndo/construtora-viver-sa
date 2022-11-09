using System.Diagnostics.CodeAnalysis;
using ConstrutoraViverSA.Domain.Enums;

namespace ConstrutoraViverSA.Domain.Dtos;

[ExcludeFromCodeCoverage]
public class EditarMaterialDto
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public TipoMaterial? Tipo { get; set; }
    public double? Valor { get; set; }
    
}