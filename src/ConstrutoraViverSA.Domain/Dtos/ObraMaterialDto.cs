namespace ConstrutoraViverSA.Domain.Dtos;

public class ObraMaterialDto
{
    public long ObraId { get; set; }
    public virtual Obra Obra { get; set; }
    public long MaterialId { get; set; }
    public virtual Material Material { get; set; }

    public int Quantidade { get; set; }

    public ObraMaterial DtoParaDominio()
    {
        return new ObraMaterial()
        {
            Obra = this.Obra,
            ObraId = this.ObraId,
            Material = this.Material,
            MaterialId = this.MaterialId,
            Quantidade = this.Quantidade
        };
    }
}