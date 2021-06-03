using ConstrutoraViverSA.Domain.Enums;
using System;

namespace ConstrutoraViverSA.Models
{
    public class ObraModel
    {
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public TipoObraEnum TipoObra { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime PrazoConclusao { get; set; }
    }
}
