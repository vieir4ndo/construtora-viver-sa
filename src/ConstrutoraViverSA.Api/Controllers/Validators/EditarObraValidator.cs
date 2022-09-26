using System;
using ConstrutoraViverSA.Api.Controllers.Requests;
using FluentValidation;

namespace ConstrutoraViverSA.Api.Controllers.Validators;

public class EditarObraValidator : AbstractValidator<ObraRequest>
{
    public EditarObraValidator()
    {
        RuleFor(x => x.Nome)
            .MaximumLength(80)
            .NotNull()
            .NotEmpty()
            .When(x => x.Nome != null);

        RuleFor(x => x.Endereco)
            .NotNull()
            .NotEmpty()
            .When(x => x.Endereco != null);

        RuleFor(x => x.TipoObra)
            .IsInEnum()
            .NotNull()
            .NotEmpty()
            .When(x => x.TipoObra != null);

        RuleFor(x => x.Descricao)
            .NotNull()
            .NotEmpty()
            .When(x => x.Descricao != null);

        RuleFor(x => x.Valor)
            .GreaterThan(0)
            .NotEmpty()
            .NotNull()
            .When(x => x.Valor != null);

        RuleFor(x => x.PrazoConclusao)
            .GreaterThan(DateTime.Today)
            .NotNull()
            .NotEmpty()
            .When(x => x.PrazoConclusao != null);

        RuleFor(x => x.OrcamentoId)
            .GreaterThan(0)
            .NotNull()
            .NotEmpty()
            .When(x => x.OrcamentoId != null);
    }
}