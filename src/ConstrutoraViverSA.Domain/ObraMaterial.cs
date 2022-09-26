namespace ConstrutoraViverSA.Domain;

public class ObraMaterial
{
    public long Id { get; set; }
    public long ObraId { get; set; }
    public virtual Obra Obra { get; set; }
    public long MaterialId { get; set; }
    public virtual Material Material { get; set; }

    public int Quantidade { get; set; }
}