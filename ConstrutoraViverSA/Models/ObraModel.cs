using ConstrutoraViverSA.Domain.Enums;
using System;

namespace ConstrutoraViverSA.Models
{
    public class ObraModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public TipoObraEnum? TipoObra { get; set; }
        public string Descricao { get; set; }
        public double? Valor { get; set; }
        public DateTime? PrazoConclusao { get; set; }

        public ObraModel
        (
            long id,
            string nome,
            string endereco,
            TipoObraEnum tipoObra,
            string descricao,
            double valor,
            DateTime prazoConclusao
         )
        {
            Id = id;
            Nome = nome;
            Endereco = endereco;
            TipoObra = tipoObra;
            Descricao = descricao;
            Valor = valor;
            PrazoConclusao = prazoConclusao;
        }

        public bool GetTipoObra(int tipo)
        {
            if ((TipoObraEnum)tipo == TipoObra)
            {
                return true;
            }

            return false;
        }
    }
}
