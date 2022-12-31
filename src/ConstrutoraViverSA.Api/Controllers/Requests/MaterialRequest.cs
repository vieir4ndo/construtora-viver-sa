#nullable enable
using ConstrutoraViverSA.Api.Controllers.Validators;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;

namespace ConstrutoraViverSA.Api.Controllers.Requests;

public class MaterialRequest
{
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public TipoMaterial? Tipo { get; set; }
    public double? Valor { get; set; }
    public int? Quantidade { get; set; }

    public void ValidarCriacao()
    {
        var resultado = new CriarMaterialValidator().Validate(this);

        if (!resultado.IsValid)
        {
            throw new ErroValidacaoException(resultado.ToString());
        }
    }
}