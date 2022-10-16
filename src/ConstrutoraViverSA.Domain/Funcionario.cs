using ConstrutoraViverSA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using ConstrutoraViverSA.Domain.Exceptions;
using ConstrutoraViverSA.Domain.Extensions;

namespace ConstrutoraViverSA.Domain;

public sealed class Funcionario
{
    public long Id { get; }
    public string Nome { get; private set; }
    public DateTime? DataNascimento { get; private set; }
    public GeneroEnum? Genero { get; private set; }
    public string Cpf { get; private set; }
    public string NumCtps { get; private set; }
    public string Endereco { get; private set; }
    public string Email { get; private set; }
    public string Telefone { get; private set; }
    public CargoEnum? Cargo { get; private set; }
    public ICollection<Obra> Obras { get; private set; }

    public Funcionario()
    {
    }

    public Funcionario(string nome, DateTime? dataNascimento, GeneroEnum? genero, string cpf, string numCtps,
        string endereco, string email, string telefone, CargoEnum? cargo)
    {
        var erros = new StringBuilder();

        if (string.IsNullOrWhiteSpace(nome))
            erros.Append("Nome inválido.");

        if (dataNascimento is null || dataNascimento == DateTime.MinValue)
            erros.Append("Data de Nascimento inválida.");

        if (genero is null)
            erros.Append("Gênero inválido.");
        
        if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11 || !CpfExtension.IsValid(cpf))
            erros.Append("CPF inválido.");

        if (string.IsNullOrWhiteSpace(numCtps))
            erros.Append("Número CTPS inválido.");

        if (string.IsNullOrWhiteSpace(endereco))
            erros.Append("Endereço inválido.");

        if (string.IsNullOrWhiteSpace(email))
            erros.Append("E-mail inválido.");

        if (string.IsNullOrWhiteSpace(telefone))
            erros.Append("Telefone inválido.");

        if (cargo is null)
            erros.Append("Cargo inválido.");

        if (erros.Length > 0)
            throw new FuncionarioInvalidoException(erros.ToString());

        Nome = nome;
        DataNascimento = dataNascimento!.Value;
        Genero = genero;
        Cpf = cpf;
        NumCtps = numCtps;
        Endereco = endereco;
        Email = email;
        Telefone = telefone;
        Cargo = cargo;
    }

    public void SetNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            return;

        Nome = nome;
    }

    public void SetDataNascimento(DateTime? dataNascimento)
    {
        if (dataNascimento is null || dataNascimento == DateTime.MinValue)
            return;

        DataNascimento = dataNascimento.Value;
    }

    public void SetGenero(GeneroEnum? genero)
    {
        if (genero is null)
            return;

        Genero = genero.Value;
    }

    public void SetCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11 || !CpfExtension.IsValid(cpf))
            return;

        Cpf = cpf;
    }

    public void SetNumCtps(string numCtps)
    {
        if (string.IsNullOrWhiteSpace(numCtps))
            return;

        NumCtps = numCtps;
    }

    public void SetEndereco(string endereco)
    {
        if (string.IsNullOrWhiteSpace(endereco))
            return;

        Endereco = endereco;
    }

    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return;

        Email = email;
    }

    public void SetTelefone(string telefone)
    {
        if (string.IsNullOrWhiteSpace(telefone))
            return;

        Telefone = telefone;
    }

    public void SetCargo(CargoEnum? cargo)
    {
        if (cargo is null)
            return;

        Cargo = cargo;
    }
}