using AutoFixture;
using ConstrutoraViverSA.Domain.Enums;

namespace ConstrutoraViverSA.Domain.Tests.Stubs;

public static class MaterialStub
{
    public static Material Valido(Fixture fixture)
    {
        var nome = fixture.Create<string>();
        var descricao = fixture.Create<string>();
        var tipo = fixture.Create<TipoMaterial>();
        var valor = fixture.Create<double>();
        var quantidade = 0;

        return new Material(nome, descricao, tipo, valor, quantidade);
    }
    
    public static Material ValidoComQuantidade(Fixture fixture, int quantidade)
    {
        var nome = fixture.Create<string>();
        var descricao = fixture.Create<string>();
        var tipo = fixture.Create<TipoMaterial>();
        var valor = fixture.Create<double>();

        return new Material(nome, descricao, tipo, valor, quantidade);
    }
}