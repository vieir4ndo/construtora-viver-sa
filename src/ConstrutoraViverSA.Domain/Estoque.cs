using System;
using ConstrutoraViverSA.Domain.Enums;

namespace ConstrutoraViverSA.Domain
{
    public class Estoque
    {
        public long Id { get; set; }
        public long MaterialId { get; set; }
        public virtual Material Material { get; set; }  
        public EntradaSaidaEnum Operacao { get; set; }
        public int Quantidade { get; set; }
        public DateTime DateTime { get; set; }
    }
}