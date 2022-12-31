using AutoFixture;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Models;

namespace ConstrutoraViverSA.Domain.Tests.Stubs;

public static class FuncionarioStub
{
    private const string CPF_VALIDO = "14533067573";
    
    public static Funcionario Valido(Fixture fixture)
    {
        var nome = fixture.Create<string>();
        var dataNascimento = fixture.Create<DateTime>();
        var genero = fixture.Create<Genero>();
        var cpf = CPF_VALIDO;
        var numCtps = fixture.Create<string>();
        var endereco = fixture.Create<string>();
        var email = fixture.Create<string>();
        var telefone = fixture.Create<string>();
        var cargo = fixture.Create<Cargo>();

        var model = new FuncionarioModel()
        {
            Nome = nome,
            DataNascimento = dataNascimento,
            Genero = genero,
            Cpf = cpf,
            NumCtps = numCtps,
            Endereco = endereco,
            Email = email,
            Telefone = telefone,
            Cargo = cargo
        };

        return new Funcionario(model);
    }

    public static Funcionario ValidoComId(Fixture fixture, long id)
    {
        var funcionario = Valido(fixture);

        funcionario.Id = id;

        return funcionario;
    }
}