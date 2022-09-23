using System;
using ConstrutoraViverSA.Domain.Enums;

namespace ConstrutoraViverSA.Domain.Dtos
{
    public class FuncionarioDto
    {
        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public GeneroEnum? Genero { get; set; }
        public string Cpf { get; set; }
        public string NumCtps { get; set; }
        public string Endereco { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public CargoEnum? Cargo { get; set; }
        
        public Funcionario DtoParaDominio()
        {
            return new Funcionario()
            {
                Nome = this.Nome,
                DataNascimento = this.DataNascimento,
                Genero = this.Genero,
                Cpf = this.Cpf,
                NumCtps = this.NumCtps,
                Endereco = this.Endereco,
                Email = this.Email,
                Telefone = this.Telefone,
                Cargo = this.Cargo
            };
        }
    }
}