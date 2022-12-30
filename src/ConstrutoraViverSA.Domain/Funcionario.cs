#nullable enable
using ConstrutoraViverSA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Exceptions;
using ConstrutoraViverSA.Domain.Extensions;

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

    public Funcionario(FuncionarioDto dto)
    {
        var erros = new StringBuilder();

        if (string.IsNullOrWhiteSpace(dto.Nome))
            erros.Append("Nome inválido.");

        if (dto.DataNascimento is null || dto.DataNascimento == DateTime.MinValue)
            erros.Append("Data de Nascimento inválida.");

        if (dto.Genero is null)
            erros.Append("Gênero inválido.");
        
        if (string.IsNullOrWhiteSpace(dto.Cpf) || dto.Cpf.Length != 11 || !CpfExtension.EhValido(dto.Cpf))
            erros.Append("CPF inválido.");

        if (string.IsNullOrWhiteSpace(dto.NumCtps))
            erros.Append("Número CTPS inválido.");

        if (string.IsNullOrWhiteSpace(dto.Endereco))
            erros.Append("Endereço inválido.");

        if (string.IsNullOrWhiteSpace(dto.Email))
            erros.Append("E-mail inválido.");

        if (string.IsNullOrWhiteSpace(dto.Telefone))
            erros.Append("Telefone inválido.");

        if (dto.Cargo is null)
            erros.Append("Cargo inválido.");

        if (erros.Length > 0)
            throw new FuncionarioInvalidoException(erros.ToString());

        Nome = dto.Nome!;
        DataNascimento = dto.DataNascimento!.Value;
        Genero = dto.Genero;
        Cpf = dto.Cpf!;
        NumCtps = dto.NumCtps!;
        Endereco = dto.Endereco!;
        Email = dto.Email!;
        Telefone = dto.Telefone!;
        Cargo = dto.Cargo;
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