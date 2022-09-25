using ConstrutoraViverSA.Domain.Enums;

namespace ConstrutoraViverSA.Domain.Dtos
{
    public class EstoqueDto
    {
        public OperacaoEstoque OperacaoEstoque { get; set; }
        public int Quantidade { get; set; }
        
        public long MaterialId { get; set; }
        
        public Estoque DtoParaDominio()
        {
            return new Estoque()
            {
               OperacaoEstoque = OperacaoEstoque,
               Quantidade = Quantidade,
               MaterialId = MaterialId
            };
        }
    }
}