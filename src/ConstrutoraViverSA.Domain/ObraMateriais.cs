namespace ConstrutoraViverSA.Domain
{
    public class ObraMateriais
    {
        public long ObraId { get; set; }
        public virtual Obra Obra { get; set; } 
        public long MaterialId { get; set; }
        public virtual Material Material { get; set; }   
    }
}