#nullable enable
using ConstrutoraViverSA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Exceptions;
using ConstrutoraViverSA.Domain.Extensions;
using ConstrutoraViverSA.Domain.Models;

namespace ConstrutoraViverSA.Domain;

public class Funcionario
{
    public long Id { get; set; }
    public string Nome { get; private set; } = null!;
    public DateTime? DataNascimento { get; private set; }
    public Genero? Genero { get; private set; }
    public string Cpf { get; private set; } = null!;
    public string NumCtps { get; private set; } = null!;
    public string Endereco { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Telefone { get; private set; } = null!;
    public Cargo? Cargo { get; private set; }
    
    public ICollection<Obra>? Obras { get; private set; }

    [ExcludeFromCodeCoverage]
    protected Funcionario()
    {
    }

    public Funcionario(FuncionarioModel model)
    {
        var erros = new StringBuilder();

        if (string.IsNullOrWhiteSpace(model.Nome))
            erros.Append("Nome inválido.");

        if (model.DataNascimento is null || model.DataNascimento == DateTime.MinValue)
            erros.Append("Data de Nascimento inválida.");

        if (model.Genero is null)
            erros.Append("Gênero inválido.");
        
        if (string.IsNullOrWhiteSpace(model.Cpf) || model.Cpf.Length != 11 || !CpfExtension.EhValido(model.Cpf))
            erros.Append("CPF inválido.");

        if (string.IsNullOrWhiteSpace(model.NumCtps))
            erros.Append("Número CTPS inválido.");

        if (string.IsNullOrWhiteSpace(model.Endereco))
            erros.Append("Endereço inválido.");

        if (string.IsNullOrWhiteSpace(model.Email))
            erros.Append("E-mail inválido.");

        if (string.IsNullOrWhiteSpace(model.Telefone))
            erros.Append("Telefone inválido.");

        if (model.Cargo is null)
            erros.Append("Cargo inválido.");

        if (erros.Length > 0)
            throw new FuncionarioInvalidoException(erros.ToString());

        Nome = model.Nome!;
        DataNascimento = model.DataNascimento!.Value;
        Genero = model.Genero;
        Cpf = model.Cpf!;
        NumCtps = model.NumCtps!;
        Endereco = model.Endereco!;
        Email = model.Email!;
        Telefone = model.Telefone!;
        Cargo = model.Cargo;
    }

    public void SetNome(string? nome)
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

    public void SetGenero(Genero? genero)
    {
        if (genero is null)
            return;

        Genero = genero.Value;
    }

    public void SetCpf(string? cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11 || !CpfExtension.EhValido(cpf))
            return;

        Cpf = cpf;
    }

    public void SetNumCtps(string? numCtps)
    {
        if (string.IsNullOrWhiteSpace(numCtps))
            return;

        NumCtps = numCtps;
    }

    public void SetEndereco(string? endereco)
    {
        if (string.IsNullOrWhiteSpace(endereco))
            return;

        Endereco = endereco;
    }

    public void SetEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return;

        Email = email;
    }

    public void SetTelefone(string? telefone)
    {
        if (string.IsNullOrWhiteSpace(telefone))
            return;

        Telefone = telefone;
    }

    public void SetCargo(Cargo? cargo)
    {
        if (cargo is null)
            return;

        Cargo = cargo;
    }
}