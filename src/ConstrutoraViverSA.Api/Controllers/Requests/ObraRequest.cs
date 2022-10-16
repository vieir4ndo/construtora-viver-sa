#nullable enable
using System;
using System.Collections.Generic;
using ConstrutoraViverSA.Api.Controllers.Validators;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;

namespace ConstrutoraViverSA.Api.Controllers.Requests;

public class ObraRequest
{
    public string Nome { get; set; }
    public string Endereco { get; set; }
    public TipoObraEnum? TipoObra { get; set; }
    public string Descricao { get; set; }
    public double? Valor { get; set; }
    public DateTime? PrazoConclusao { get; set; }
    public long? OrcamentoId { get; set; }
    public List<long>? Funcionarios { get; set; }
    public Dictionary<long, int>? Materiais { get; set; }

    public void ValidarCriacao()
    {
        var resultado = new CriarObraValidator().Validate(this);

        if (resultado.IsValid == false)
        {
            throw new ErroValidacaoException(resultado.ToString());
        }
    }

    public void ValidarEdicao()
    {
        var resultado = new EditarObraValidator().Validate(this);

        if (resultado.IsValid == false)
        {
            throw new ErroValidacaoException(resultado.ToString());
        }
    }
}