using ConstrutoraViverSA.Domain.Enums;
using System;

namespace ConstrutoraViverSA.Domain
{
    public class Material
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public TipoMaterialEnum? Tipo { get; set; }
        public double? Valor { get; set; }
        public DateTime? DataValidade { get; set; }

        public Material() { }
        public Material
       (
           string nome,
           string descricao,
           TipoMaterialEnum tipo,
           double valor,
           DateTime dataValidade
       )
        {
            Nome = nome;
            Descricao = descricao;
            Tipo = tipo;
            Valor = valor;
            DataValidade = dataValidade;
        }
    }
}
