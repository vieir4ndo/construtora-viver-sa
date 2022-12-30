using AutoFixture;
using ConstrutoraViverSA.Domain.Enums;

namespace ConstrutoraViverSA.Domain.Tests.Stubs;

public static class ObraStub
{
    public static Obra Valido(Fixture fixture)
    {
        var nome = fixture.Create<string>();
        var endereco = fixture.Create<string>();
        var tipoObra = fixture.Create<TipoObra>();
        var descricao = fixture.Create<string>();
        var valor = fixture.Create<double>();
        var orcamento = new Orcamento("teste", "teste", TipoObra.Ambas, DateTime.Today, DateTime.Today.AddDays(1),
            10.85);
        var prazoConclusao = DateTime.Today.AddDays(2);

        return new Obra(nome, endereco, tipoObra, descricao, valor, prazoConclusao, orcamento, null, null);
    }
}