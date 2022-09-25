using System;
using ConstrutoraViverSA.Domain.Enums;

namespace ConstrutoraViverSA.Domain.Dtos
{
    public class ObraDto 
    {
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public TipoObraEnum? TipoObra { get; set; }
        public string Descricao { get; set; }
        public double? Valor { get; set; }
        public DateTime? PrazoConclusao { get; set; }

        public Obra DtoParaDominio()
        {
            return new Obra()
            {
                Nome = Nome,
                Endereco = Endereco,
                TipoObra = TipoObra,
                Descricao = Descricao,
                Valor = Valor,
                PrazoConclusao = PrazoConclusao
            };
        }
    }
}