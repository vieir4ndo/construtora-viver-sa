using ConstrutoraViverSA.Domain.Enums;
using System;

namespace ConstrutoraViverSA.Models
{
    public class MaterialModel
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public TipoMaterialEnum? Tipo { get; set; }
        public double? Valor { get; set; }
        public DateTime? DataValidade { get; set; }

        public MaterialModel
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
