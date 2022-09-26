using ConstrutoraViverSA.Domain.Enums;
using System;
using System.Collections.Generic;

namespace ConstrutoraViverSA.Domain;

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

    public virtual ICollection<Obra> Obras { get; set; }

    public Funcionario()
    {
    }

    public Funcionario
    (
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
}