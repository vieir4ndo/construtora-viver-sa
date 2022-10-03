using ConstrutoraViverSA.Domain.Enums;

namespace ConstrutoraViverSA.Domain.Dtos;

public class MaterialDto
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public TipoMaterialEnum? Tipo { get; set; }
    public double? Valor { get; set; }
}