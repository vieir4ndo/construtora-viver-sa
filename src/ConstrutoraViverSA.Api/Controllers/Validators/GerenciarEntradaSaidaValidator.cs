using ConstrutoraViverSA.Api.Controllers.Requests;
using FluentValidation;

namespace ConstrutoraViverSA.Api.Controllers.Validators;

public class GerenciarEntradaSaidaValidator : AbstractValidator<EntradaSaidaMaterialRequest>
{
    public GerenciarEntradaSaidaValidator()
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