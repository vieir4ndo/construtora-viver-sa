using System;
using ConstrutoraViverSA.Domain.Enums;

namespace ConstrutoraViverSA.Domain.Dtos
{
    public class EntradaSaidaMaterialDto
    {
        public EntradaSaidaEnum EntradaSaidaEnum { get; set; }
        public int Quantidade { get; set; }
        
        public long MaterialId { get; set; }
        
        public Estoque DtoParaDominio()
        {
            return new Estoque()
            {
               EntradaSaidaEnum = EntradaSaidaEnum,
               Quantidade = Quantidade,
               MaterialId = MaterialId,
               DateTime = DateTime.Now
            };
        }
    }
}