using System.Diagnostics.CodeAnalysis;
using ConstrutoraViverSA.Domain.Enums;

namespace ConstrutoraViverSA.Domain.Dtos;

[ExcludeFromCodeCoverage]
public class EntradaSaidaMaterialDto
{
    public EntradaSaidaEnum? Operacao { get; set; }
    public int? Quantidade { get; set; }
    public long? MaterialId { get; set; }
}