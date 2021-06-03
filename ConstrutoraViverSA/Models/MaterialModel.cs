using ConstrutoraViverSA.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ConstrutoraViverSA.Models
{
    public class MaterialModel
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public TipoMaterialEnum? Tipo { get; set; }
        public double? Valor { get; set; }
        public DateTime? DataValidade { get; set; }

        public MaterialModel() { }
    }
}
