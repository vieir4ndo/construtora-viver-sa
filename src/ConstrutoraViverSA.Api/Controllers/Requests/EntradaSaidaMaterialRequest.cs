using ConstrutoraViverSA.Api.Controllers.Validators;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;

namespace ConstrutoraViverSA.Api.Controllers.Requests;

public class EntradaSaidaMaterialRequest
{
    public EntradaSaida? Operacao { get; set; }
    public int? Quantidade { get; set; }

    public void Validar()
    {
        var resultado = new EntradaSaidaMaterialValidator().Validate(this);

        if (!resultado.IsValid) throw new ErroValidacaoException(resultado.ToString());
    }
}