using ConstrutoraViverSA.Domain.Enums;
using System;

namespace ConstrutoraViverSA.Domain
{
    public class Funcionario
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
    }
}
