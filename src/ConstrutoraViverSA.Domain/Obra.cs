#nullable enable
using ConstrutoraViverSA.Domain.Enums;
using System;
using System.Collections.Generic;

namespace ConstrutoraViverSA.Domain
{
    public class Obra
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public TipoObraEnum? TipoObra { get; set; }
        public string Descricao { get; set; }
        public double? Valor { get; set; }
        public DateTime? PrazoConclusao { get; set; }
        public virtual Orcamento Orcamento { get; set; }
        public long OrcamentoId { get; set; }

        public virtual ICollection<Funcionario> Funcionarios { get; set; }

        public virtual ICollection<ObraMaterial> ObraMateriais { get; set; }

        public Obra()
        {
        }

        public Obra
        (
            string nome,
            string endereco,
            TipoObraEnum tipoObra,
            string descricao,
            double valor,
            DateTime prazoConclusao,
            long orcamentoId
        )
        {
            Nome = nome;
            Endereco = endereco;
            TipoObra = tipoObra;
            Descricao = descricao;
            Valor = valor;
            PrazoConclusao = prazoConclusao;
            OrcamentoId = orcamentoId;
        }
    }
}