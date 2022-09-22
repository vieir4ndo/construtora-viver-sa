using ConstrutoraViverSA.Domain.Enums;
using System;

namespace ConstrutoraViverSA.App.Models
{
    public class OrcamentoModel
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public string Endereco { get; set; }
        public TipoObraEnum? TipoObra { get; set; }
        public DateTime? DataEmissao { get; set; }
        public DateTime? DataValidade { get; set; }
        public double? Valor { get; set; }

        public OrcamentoModel
       (
           long id,
           string descricao,
           string endereco,
           TipoObraEnum tipoObra,
           DateTime dataEmissao,
           DateTime dataValidade,
           double valor
       )
        {
            Id = id;
            Descricao = descricao;
            Endereco = endereco;
            TipoObra = tipoObra;
            DataEmissao = dataEmissao;
            DataValidade = dataValidade;
            Valor = valor;
        }
    }
}
