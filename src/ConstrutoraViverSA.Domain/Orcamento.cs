using System;
using ConstrutoraViverSA.Domain.Enums;

namespace ConstrutoraViverSA.Domain
{
    public class Orcamento
    {
        public Orcamento() {}
        public Orcamento
        (
            string descricao,
            string endereco,
            TipoObraEnum? tipoObra,
            DateTime dataEmissao,
            DateTime dataValidade,
            double valor
        )
        {
            Descricao = descricao;
            Endereco = endereco;
            TipoObra = tipoObra;
            DataEmissao = dataEmissao;
            DataValidade = dataValidade;
            Valor = valor;
        }

        public long Id { get; set; }
        public string Descricao { get; set; }
        public string Endereco { get; set; }
        public TipoObraEnum? TipoObra { get; set; }
        public DateTime? DataEmissao { get; set; }
        public DateTime? DataValidade { get; set; }
        public double? Valor { get; set; }
        public virtual Obra Obra { get; set; }
    }
}