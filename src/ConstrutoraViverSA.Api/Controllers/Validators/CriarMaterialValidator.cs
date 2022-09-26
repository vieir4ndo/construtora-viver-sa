using ConstrutoraViverSA.Api.Controllers.Requests;
using FluentValidation;

namespace ConstrutoraViverSA.Api.Controllers.Validators;

public class CriarMaterialValidator : AbstractValidator<MaterialRequest>
{
    public CriarMaterialValidator()
    {
        RuleFor(x => x.Nome)
            .MaximumLength(80)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Descricao)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Tipo)
            .IsInEnum()
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Valor)
            .GreaterThan(0)
            .NotEmpty()
            .NotNull();
    }
}