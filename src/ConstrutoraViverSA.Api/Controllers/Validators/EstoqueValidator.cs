using ConstrutoraViverSA.Api.Controllers.Requests;
using FluentValidation;

namespace ConstrutoraViverSA.Api.Controllers.Validators
{
    public class EstoqueValidator: AbstractValidator<EstoqueRequest>
    {
        public EstoqueValidator()
        {
            RuleFor(x => x.OperacaoEstoque)
                .IsInEnum()
                .NotEmpty()
                .NotNull();
            
            RuleFor(x => x.Quantidade)
                .GreaterThan(0)
                .NotNull()
                .NotEmpty();
        }
    }
}