#nullable enable
using ConstrutoraViverSA.Api.Controllers.Validators;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;

namespace ConstrutoraViverSA.Api.Controllers.Requests;

public class EditarMaterialRequest
{
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public TipoMaterial? Tipo { get; set; }
    public double? Valor { get; set; }

    public void ValidarEdicao()
    {
        var resultado = new EditarMaterialValidator().Validate(this);

        if (!resultado.IsValid)
        {
            throw new ErroValidacaoException(resultado.ToString());
        }
    }
}