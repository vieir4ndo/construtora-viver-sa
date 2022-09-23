using System;
using ConstrutoraViverSA.Domain.Enums;

namespace ConstrutoraViverSA.Domain.Dtos
{
    public class MaterialDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public TipoMaterialEnum? Tipo { get; set; }
        public double? Valor { get; set; }
        public DateTime? DataValidade { get; set; }

        public Material DtoParaDominio()
        {
            return new Material()
            {
                Nome = this.Nome,
                Descricao = this.Descricao,
                Tipo = this.Tipo,
                Valor = this.Valor,
                DataValidade = this.DataValidade
            };
        }
    }
}