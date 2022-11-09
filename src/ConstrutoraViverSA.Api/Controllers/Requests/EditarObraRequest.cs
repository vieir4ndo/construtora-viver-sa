#nullable enable
using System;
using System.Collections.Generic;
using ConstrutoraViverSA.Api.Controllers.Validators;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;

namespace ConstrutoraViverSA.Api.Controllers.Requests;

public class EditarObraRequest
{
    public string Nome { get; set; }
    public string Endereco { get; set; }
    public TipoObra? TipoObra { get; set; }
    public string Descricao { get; set; }
    public double? Valor { get; set; }
    public DateTime? PrazoConclusao { get; set; }
    public long? OrcamentoId { get; set; }

    public void ValidarEdicao()
    {
        var resultado = new EditarObraValidator().Validate(this);

        if (!resultado.IsValid)
        {
            throw new ErroValidacaoException(resultado.ToString());
        }
    }
}