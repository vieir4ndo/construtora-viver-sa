using AutoFixture;
using ConstrutoraViverSA.Domain.Enums;

namespace ConstrutoraViverSA.Domain.Tests.Stubs;

public static class OrcamentoStub
{
    public static Orcamento Valido(Fixture fixture)
    {
        var descricao = fixture.Create<string>();
        var endereco = fixture.Create<string>();
        var tipoObra = fixture.Create<TipoObra>();
        var dataEmissao = DateTime.Today;
        var dataValidade = DateTime.Today.AddDays(1);
        var valor = fixture.Create<double>();

        return new Orcamento(descricao, endereco, tipoObra, dataEmissao, dataValidade, valor);
    }

    public static Orcamento ValidoComDataDeEmissaoEValidade(Fixture fixture, DateTime dataEmissao, DateTime dataValidade)
    {
        var descricao = fixture.Create<string>();
        var endereco = fixture.Create<string>();
        var tipoObra = fixture.Create<TipoObra>();
        var valor = fixture.Create<double>();

        return new Orcamento(descricao, endereco, tipoObra, dataEmissao, dataValidade, valor);
    }
    
    public static Orcamento ValidoComId(Fixture fixture, long id)
    {
        var orcamento = Valido(fixture);

        orcamento.Id = id;

        return orcamento;
    }
}