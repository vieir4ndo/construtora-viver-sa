using ConstrutoraViverSA.Domain.Enums;
using System;

namespace ConstrutoraViverSA.Domain
{
    public class Material
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public TipoMaterialEnum? Tipo { get; set; }
        public double? Valor { get; set; }
        public DateTime? DataValidade { get; set; }
    }
}
