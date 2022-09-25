using ConstrutoraViverSA.Api.Controllers.Requests;
using FluentValidation;

namespace ConstrutoraViverSA.Api.Controllers.Validators
{
    public class CriarFuncionarioValidator : AbstractValidator<FuncionarioRequest>
    {
        public CriarFuncionarioValidator()
        {
            RuleFor(p => p.Nome)
                .MaximumLength(80)
                .NotNull()
                .NotEmpty();
            
            RuleFor(p => p.DataNascimento)
                .NotEmpty()
                .NotNull();
            
            RuleFor(p => p.Genero)
                .IsInEnum()
                .NotEmpty()
                .NotNull();
            
            RuleFor(p => p.Cpf)
                .IsValidCPF()
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.NumCtps)
                .NotEmpty()
                .NotNull();
            
            RuleFor(p => p.Endereco)
                .NotEmpty()
                .NotNull();
            
            RuleFor(p => p.Email)
                .EmailAddress()
                .NotEmpty()
                .NotNull();
            
            RuleFor(p => p.Telefone)
                .NotEmpty()
                .NotNull();
            
            RuleFor(p => p.Cargo)
                .IsInEnum()
                .NotEmpty()
                .NotNull();
        }
    }
}