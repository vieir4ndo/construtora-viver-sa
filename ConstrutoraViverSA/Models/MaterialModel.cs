using ConstrutoraViverSA.Domain.Enums;
using System;

namespace ConstrutoraViverSA.Models
{
    public class MaterialModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public TipoMaterialEnum? Tipo { get; set; }
        public double? Valor { get; set; }
        public DateTime? DataValidade { get; set; }
        public MaterialModel
         (
             long id,
             string nome,
             string descricao,
             TipoMaterialEnum tipo,
             double valor,
             DateTime dataValidade
         )
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Tipo = tipo;
            Valor = valor;
            DataValidade = dataValidade;
        }

        public bool GetTipo(int tipo)
        {
            if ((TipoMaterialEnum)tipo == Tipo)
            {
                return true;
            }

            return false;
        }
    }
}
