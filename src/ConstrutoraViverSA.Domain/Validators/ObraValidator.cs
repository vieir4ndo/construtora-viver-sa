using FluentValidation;

namespace ConstrutoraViverSA.Domain.Validators;

public class ObraValidator : AbstractValidator<Obra>
{
    public ObraValidator()
    {
        RuleFor(x => x.Nome)
            .MaximumLength(80)
            .NotEmpty()
            .NotNull()
            .WithMessage("Nome inválido.");
        
        RuleFor(x => x.Endereco)
            .NotNull()
            .NotEmpty()
            .WithMessage("Endereço inválido.");

        RuleFor(x => x.TipoObra)
            .IsInEnum()
            .NotNull()
            .NotEmpty()
            .WithMessage("Tipo Obra inválido.");

        RuleFor(x => x.Descricao)
            .NotNull()
            .NotEmpty()
            .WithMessage("Descrição inválida.");

        RuleFor(x => x.Valor)
            .GreaterThan(0)
            .NotEmpty()
            .NotNull()
            .WithMessage("Valor inválido.");

        RuleFor(x => x.PrazoConclusao)
            .GreaterThan(x => x.Orcamento.DataValidade)
            .NotNull()
            .NotEmpty()
            .WithMessage("Prazo conclusão inválido.")
            .When(x => x.Orcamento is not null);

        RuleFor(x => x.Orcamento)
            .NotNull()
            .WithMessage("Orcamento inválido.");
    }
}