using System;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Enums;

namespace ConstrutoraViverSA.Api.Requests
{
    public class CadastroOrcamentoRequest
    {
        public string Descricao { get; set; }
        public string Endereco { get; set; }
        public TipoObraEnum TipoObra { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataValidade { get; set; }
        public double Valor { get; set; }

        public OrcamentoDto RequestParaDto()
        {
            return new OrcamentoDto()
            {
                Descricao = this.Descricao,
                Endereco = this.Endereco,
                TipoObra = this.TipoObra,
                DataEmissao = this.DataEmissao,
                DataValidade = this.DataValidade,
                Valor = this.Valor
            };
        }
    }
}