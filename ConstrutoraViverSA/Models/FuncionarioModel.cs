using ConstrutoraViverSA.Domain.Enums;
using System;

namespace ConstrutoraViverSA.Models
{
    public class FuncionarioModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public GeneroEnum? Genero { get; set; }
        public string Cpf { get; set; }
        public string NumCtps { get; set; }
        public string Endereco { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public CargoEnum? Cargo { get; set; }

        public FuncionarioModel
           (
           long id,
           string nome,
           DateTime dataNascimento,
           GeneroEnum genero,
           string cpf,
           string numCtps,
           string endereco,
           string email,
           string telefone,
           CargoEnum cargo
           )
        {
            Id = id;
            Nome = nome;
            DataNascimento = dataNascimento;
            Genero = genero;
            Cpf = cpf;
            NumCtps = numCtps;
            Endereco = endereco;
            Email = email;
            Telefone = telefone;
            Cargo = cargo;
        }

        public bool GetCargo(int tipo)
        {
            if ((CargoEnum)tipo == Cargo)
            {
                return true;
            }

            return false;
        }
        public bool GetGenero(int tipo)
        {
            if ((GeneroEnum)tipo == Genero)
            {
                return true;
            }

            return false;
        }
    }
}
