using ConstrutoraViverSA.Api.Controllers.Requests;
using FluentValidation;

namespace ConstrutoraViverSA.Api.Controllers.Validators;

public class EditarFuncionarioValidator : AbstractValidator<FuncionarioRequest>
{
    public EditarFuncionarioValidator()
    {
        RuleFor(p => p.Nome)
            .MaximumLength(80)
            .NotNull()
            .NotEmpty()
            .When(x => x.Nome != null);

        RuleFor(p => p.DataNascimento)
            .NotEmpty()
            .NotNull()
            .When(x => x.DataNascimento != null);

        RuleFor(p => p.Genero)
            .IsInEnum()
            .NotEmpty()
            .NotNull()
            .When(x => x.Genero != null);

        RuleFor(p => p.Cpf)
            .IsValidCPF()
            .NotNull()
            .NotEmpty()
            .When(x => x.Cpf != null);

        RuleFor(p => p.NumCtps)
            .NotEmpty()
            .NotNull()
            .When(x => x.NumCtps != null);

        RuleFor(p => p.Endereco)
            .NotEmpty()
            .NotNull()
            .When(x => x.Endereco != null);

        RuleFor(p => p.Email)
            .EmailAddress()
            .NotEmpty()
            .NotNull()
            .When(x => x.Email != null);

        RuleFor(p => p.Telefone)
            .NotEmpty()
            .NotNull()
            .When(x => x.Telefone != null);

        RuleFor(p => p.Cargo)
            .IsInEnum()
            .NotEmpty()
            .NotNull()
            .When(x => x.Cargo != null);
    }
}