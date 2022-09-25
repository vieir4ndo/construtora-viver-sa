using ConstrutoraViverSA.Domain.Enums;

namespace ConstrutoraViverSA.Domain
{
    public class Estoque
    {
        public long Id { get; set; }
        public long MaterialId { get; set; }
        public virtual Material Material { get; set; }  
        public OperacaoEstoque OperacaoEstoque { get; set; }
        public int Quantidade { get; set; }
    }
}