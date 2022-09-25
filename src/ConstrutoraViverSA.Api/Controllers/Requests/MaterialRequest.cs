using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Enums;

namespace ConstrutoraViverSA.Api.Controllers.Requests
{
    public class MaterialRequest
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public TipoMaterialEnum? Tipo { get; set; }
        public double? Valor { get; set; }


        public MaterialDto RequestParaDto()
        {
            return new MaterialDto()
            {
                Nome = this.Nome,
                Descricao = this.Descricao,
                Tipo = this.Tipo,
                Valor = this.Valor,
            };
        }
    }
}