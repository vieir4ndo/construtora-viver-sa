using ConstrutoraViverSA.Api.Controllers.Requests;
using FluentValidation;

namespace ConstrutoraViverSA.Api.Controllers.Validators
{
    
    public class EditarFuncionarioValidator: AbstractValidator<FuncionarioRequest>
    {
        public EditarFuncionarioValidator()
        {
            RuleFor(p => p.Nome)
                .MaximumLength(80);
            
            RuleFor(p => p.DataNascimento);
            
            RuleFor(p => p.Genero)
                .IsInEnum();
            
            RuleFor(p => p.Cpf)
                .IsValidCPF();

            RuleFor(p => p.NumCtps);
            
            RuleFor(p => p.Endereco);
            
            RuleFor(p => p.Email)
                .EmailAddress();
            
            RuleFor(p => p.Telefone);
            
            RuleFor(p => p.Cargo)
                .IsInEnum();
        }
    }
}