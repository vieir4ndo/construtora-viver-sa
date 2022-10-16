using System;
using System.Diagnostics.CodeAnalysis;
using ConstrutoraViverSA.Domain.Enums;

namespace ConstrutoraViverSA.Domain.Dtos;

[ExcludeFromCodeCoverage]
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
}