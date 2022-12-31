using System;
using System.Data;
using ConstrutoraViverSA.Api.Controllers.Requests;
using FluentValidation;

namespace ConstrutoraViverSA.Api.Controllers.Validators;

public class CriarObraValidator : AbstractValidator<ObraRequest>
{
    public CriarObraValidator()
    {
        RuleFor(x => x.Nome)
            .MaximumLength(80)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Endereco)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.TipoObra)
            .IsInEnum()
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Descricao)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Valor)
            .GreaterThan(0)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.PrazoConclusao)
            .GreaterThan(DateTime.Today)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.OrcamentoId)
            .GreaterThan(0)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Funcionarios)
            .NotEmpty()
            .NotNull()
            .When(x => x.Funcionarios != null);
        
        RuleFor(x => x.Materiais)
            .NotEmpty()
            .NotNull()
            .When(x => x.Materiais != null);
    }
}