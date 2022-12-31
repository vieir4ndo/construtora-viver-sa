using AutoFixture;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Models;

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

        var model = new ObraModel()
        {
            Nome = nome,
            Endereco = endereco, 
            TipoObra = tipoObra,
            Descricao = descricao,
            Valor = valor,
            PrazoConclusao = prazoConclusao,
            Orcamento = orcamento,
            Funcionarios = null,
            Materiais = null
        };
        
        return new Obra(model);
    }

    public static Obra ValidoComPrazoDeConclusao(Fixture fixture, DateTime prazoConclusao)
    {
        var nome = fixture.Create<string>();
        var endereco = fixture.Create<string>();
        var tipoObra = fixture.Create<TipoObra>();
        var descricao = fixture.Create<string>();
        var valor = fixture.Create<double>();
        var orcamento = new Orcamento("teste", "teste", TipoObra.Ambas, prazoConclusao.AddDays(-2), prazoConclusao.AddDays(-1),
            10.85);

        var model = new ObraModel()
        {
            Nome = nome,
            Endereco = endereco, 
            TipoObra = tipoObra,
            Descricao = descricao,
            Valor = valor,
            PrazoConclusao = prazoConclusao,
            Orcamento = orcamento,
            Funcionarios = null,
            Materiais = null
        };
        
        return new Obra(model);
    }

    public static Obra ValidoComFuncionarios(Fixture fixture, IList<Funcionario> funcionarios)
    {
        var obra = Valido(fixture);

        foreach (var funcionario in funcionarios)
        {
            obra.AlocarFuncionario(funcionario);
        }

        return obra;
    }
    
    public static Obra ValidoComMateriais(Fixture fixture, IDictionary<Material, int> materiais)
    {
        var obra = Valido(fixture);

        foreach (var material in materiais)
        {
            obra.AlocarMaterial(material.Key, material.Value);
        }

        return obra;
    }

    public static Obra ValidoComFuncionariosEMateriais(Fixture fixture, IList<Funcionario> funcionarios, IDictionary<Material, int> materiais)
    {
        var obra = Valido(fixture);

        foreach (var material in materiais)
        {
            obra.AlocarMaterial(material.Key, material.Value);
        }
        
        foreach (var funcionario in funcionarios)
        {
            obra.AlocarFuncionario(funcionario);
        }

        return obra;
    }
}