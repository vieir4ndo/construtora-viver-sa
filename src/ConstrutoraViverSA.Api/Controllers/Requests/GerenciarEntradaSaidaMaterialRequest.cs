using ConstrutoraViverSA.Api.Controllers.Validators;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;

namespace ConstrutoraViverSA.Api.Controllers.Requests;

public class GerenciarEntradaSaidaMaterialRequest
{
    public EntradaSaidaEnum Operacao { get; set; }
    public int Quantidade { get; set; }

    public EntradaSaidaMaterialDto RequestParaDto()
    {
        return new EntradaSaidaMaterialDto
        {
            Operacao = Operacao,
            Quantidade = Quantidade
        };
    }

    public void Validar()
    {
        var resultado = new GerenciarEntradaSaidaValidator().Validate(this);

        if (resultado.IsValid == false) throw new ErroValidacaoException(resultado.ToString());
    }
}