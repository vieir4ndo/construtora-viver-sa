using ConstrutoraViverSA.Domain.Enums;
using System;

namespace ConstrutoraViverSA.Domain
{
    public class Obra
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public TipoObraEnum TipoObra { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime PrazoConclusao { get; set; }
    }
}
