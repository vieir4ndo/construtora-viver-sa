using ConstrutoraViverSA.Api.Controllers.Requests;
using FluentValidation;

namespace ConstrutoraViverSA.Api.Controllers.Validators
{
    public class EditarOrcamentoValidator: AbstractValidator<OrcamentoRequest>
    {
        public EditarOrcamentoValidator()
        {
            RuleFor(x => x.Descricao)
                .NotEmpty()
                .NotNull()
                .When(x => x.Descricao != null);
            
            RuleFor(x => x.Endereco)
                .NotEmpty()
                .NotNull()
                .When(x => x.Endereco != null);
            
            RuleFor(x => x.TipoObra)
                .IsInEnum()
                .NotEmpty()
                .NotNull()
                .When(x => x.TipoObra != null);
            
            RuleFor(x => x.DataEmissao)
                .LessThan(x => x.DataValidade)
                .NotEmpty()
                .NotNull()
                .When(x => x.DataEmissao != null);
            
            RuleFor(x => x.DataValidade)
                .GreaterThan(x => x.DataEmissao)
                .NotEmpty()
                .NotNull()
                .When(x => x.DataValidade != null);
            
            RuleFor(x => x.Valor)
                .GreaterThan(0)
                .NotEmpty()
                .NotNull()
                .When(x => x.Valor != null);
        }
    }
}