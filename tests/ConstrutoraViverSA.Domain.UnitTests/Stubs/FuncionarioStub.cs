using AutoFixture;
using ConstrutoraViverSA.Domain.Enums;

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

        return new Funcionario(nome, dataNascimento, genero, cpf, numCtps, endereco, email, telefone, cargo);
    }

    public static Funcionario ValidoComId(Fixture fixture, long id)
    {
        var funcionario = Valido(fixture);

        funcionario.Id = id;

        return funcionario;
    }
}