using ConstrutoraViverSA.Domain.Enums;
using System;

namespace ConstrutoraViverSA.Models
{
    public class OrcamentoModel
    {
        public string Descricao { get; set; }
        public string Endereco { get; set; }
        public TipoObraEnum? TipoObra { get; set; }
        public DateTime? DataEmissao { get; set; }
        public DateTime? DataValidade { get; set; }
        public double? Valor { get; set; }
    }
}
