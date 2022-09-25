using ConstrutoraViverSA.Api.Controllers.Requests;
using FluentValidation;

namespace ConstrutoraViverSA.Api.Controllers.Validators
{
    public class EditarMaterialValidator: AbstractValidator<MaterialRequest>
    {
        public EditarMaterialValidator()
        {
            RuleFor(x => x.Nome)
                .MaximumLength(80)
                .NotEmpty()
                .NotNull()
                .When(x => x.Nome != null);
            
            RuleFor(x => x.Descricao)
                .NotEmpty()
                .NotNull()
                .When(x => x.Descricao != null);
            
            RuleFor(x => x.Tipo)
                .IsInEnum()
                .NotEmpty()
                .NotNull()
                .When(x => x.Tipo != null);

            RuleFor(x => x.Valor)
                .GreaterThan(0)
                .NotEmpty()
                .NotNull()
                .When(x => x.Valor != null);
        }
    }
}