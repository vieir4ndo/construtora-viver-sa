using ConstrutoraViverSA.Api.Controllers.Requests;
using FluentValidation;

namespace ConstrutoraViverSA.Api.Controllers.Validators
{
    public class CriarOrcamentoValidator: AbstractValidator<OrcamentoRequest>
    {
        public CriarOrcamentoValidator()
        {
            RuleFor(x => x.Descricao)
                .NotEmpty()
                .NotNull();
            
            RuleFor(x => x.Endereco)
                .NotEmpty()
                .NotNull();
            
            RuleFor(x => x.TipoObra)
                .IsInEnum()
                .NotEmpty()
                .NotNull();
            
            RuleFor(x => x.DataEmissao)
                .LessThan(x => x.DataValidade)
                .NotEmpty()
                .NotNull();
            
            RuleFor(x => x.DataValidade)
                .GreaterThan(x => x.DataEmissao)
                .NotEmpty()
                .NotNull();
            
            RuleFor(x => x.Valor)
                .GreaterThan(0)
                .NotEmpty()
                .NotNull();
        }
    }
}