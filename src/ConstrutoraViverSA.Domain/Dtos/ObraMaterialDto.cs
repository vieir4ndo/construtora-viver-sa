using System.Diagnostics.CodeAnalysis;

namespace ConstrutoraViverSA.Domain.Dtos;

[ExcludeFromCodeCoverage]
public class ObraMaterialDto
{
    public long ObraId { get; set; }
    public virtual Obra Obra { get; set; }
    public long MaterialId { get; set; }
    public virtual Material Material { get; set; }
    public int Quantidade { get; set; }
}