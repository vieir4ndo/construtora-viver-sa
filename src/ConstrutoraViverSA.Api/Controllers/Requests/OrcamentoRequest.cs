using System;
using ConstrutoraViverSA.Api.Controllers.Validators;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;

namespace ConstrutoraViverSA.Api.Controllers.Requests;

public class OrcamentoRequest
{
    public string Descricao { get; set; }
    public string Endereco { get; set; }
    public TipoObraEnum? TipoObra { get; set; }
    public DateTime? DataEmissao { get; set; }
    public DateTime? DataValidade { get; set; }
    public double? Valor { get; set; }

    public void ValidarCriacao()
    {
        var resultado = new CriarOrcamentoValidator().Validate(this);

        if (resultado.IsValid == false)
        {
            throw new ErroValidacaoException(resultado.ToString());
        }
    }

    public void ValidarEdicao()
    {
        var resultado = new EditarOrcamentoValidator().Validate(this);

        if (resultado.IsValid == false)
        {
            throw new ErroValidacaoException(resultado.ToString());
        }
    }
}