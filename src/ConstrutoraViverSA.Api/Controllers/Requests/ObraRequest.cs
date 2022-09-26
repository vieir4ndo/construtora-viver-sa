using System;
using ConstrutoraViverSA.Api.Controllers.Validators;
using ConstrutoraViverSA.Domain.Dtos;
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

    public ObraDto RequestParaDto()
    {
        return new ObraDto()
        {
            Nome = this.Nome,
            Endereco = this.Endereco,
            TipoObra = this.TipoObra,
            Descricao = this.Descricao,
            Valor = this.Valor,
            PrazoConclusao = this.PrazoConclusao,
            OrcamentoId = this.OrcamentoId
        };
    }

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