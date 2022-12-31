using ConstrutoraViverSA.Api.Controllers.Requests;
using FluentValidation;

namespace ConstrutoraViverSA.Api.Controllers.Validators;

public class EntradaSaidaMaterialValidator : AbstractValidator<EntradaSaidaMaterialRequest>
{
    public EntradaSaidaMaterialValidator()
    {
        RuleFor(x => x.Operacao)
            .IsInEnum()
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Quantidade)
            .GreaterThan(0)
            .NotNull()
            .NotEmpty();
    }
}