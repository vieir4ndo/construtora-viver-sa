using AutoFixture;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace ConstrutoraViverSA.Domain.Tests;

public class ObraMaterialTests
{
    private readonly Fixture _fixture = new Fixture();

    [Fact]
    public void Construtor_ComDadosValidos_DeveConstruirCorretamente()
    {
        var obra = _fixture.Create<Obra>();
        var quantidade = 10;
        var quantidadeMaterial = 10;
        var valorMaterial = 10.95;
        var material = new Material("teste", "teste", TipoMaterial.Cimento, valorMaterial, quantidadeMaterial);
        var operacao = EntradaSaida.Entrada;

        var result = new ObraMaterial(obra, material,quantidade, operacao);

        result.Should().NotBeNull();
        result.Obra.Should().BeEquivalentTo(obra);
        result.ObraId.Should().Be(obra.Id);
        result.Material.Should().BeEquivalentTo(material);
        result.MaterialId.Should().Be(material.Id);
        result.Quantidade.Should().Be(quantidade);
        result.Operacao.Should().Be(operacao);
        result.DataHora.Should().NotBe(null);
        result.Material.Quantidade.Should().Be(quantidadeMaterial - quantidade);
    }
    
    [Fact]
    public void Construtor_EntradaNaObraComQuantidadeDeMaterialInsuficiente_DeveLancarExcecao()
    {
        var obra = _fixture.Create<Obra>();
        var quantidade = 11;
        var quantidadeMaterial = 10;
        var valorMaterial = 10.95;
        var operacao = EntradaSaida.Entrada;
        var material = new Material("teste", "teste", TipoMaterial.Cimento, valorMaterial, quantidadeMaterial);
        ObraMaterial? obraMaterial = null;

        var result = () =>
        {
            obraMaterial = new ObraMaterial(obra, material, quantidade, operacao);
        };

        obraMaterial.Should().BeNull();
        result.Should().Throw<OperacaoInvalidaException>();
    }
    
    [Fact]
    public void Construtor_ComObraInvalida_DeveLancarExcecao()
    {
        var quantidade = 11;
        var quantidadeMaterial = 10;
        var valorMaterial = 10.95;
        var material = new Material("teste", "teste", TipoMaterial.Cimento, valorMaterial, quantidadeMaterial);
        var operacao = _fixture.Create<EntradaSaida>();
        ObraMaterial? obraMaterial = null;
        
        var result = () =>
        {
            obraMaterial = new ObraMaterial(null, material, quantidade, operacao);
        };

        obraMaterial.Should().BeNull();
        result.Should().Throw<ObraMaterialInvalidaException>();
    }
    
    [Fact]
    public void Construtor_ComMaterialInvalido_DeveLancarExcecao()
    {
        var obra = _fixture.Create<Obra>();
        var quantidade = 11;
        var operacao = _fixture.Create<EntradaSaida>();
        ObraMaterial? obraMaterial = null;
        
        var result = () =>
        {
            obraMaterial = new ObraMaterial(obra, null, quantidade, operacao);
        };

        obraMaterial.Should().BeNull();
        result.Should().Throw<ObraMaterialInvalidaException>();
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData(-1)]
    [InlineData(0)]
    public void Construtor_ComQuantidadeInvalida_DeveLancarExcecao(int? quantidade)
    {
        var obra = _fixture.Create<Obra>();
        var quantidadeMaterial = 10;
        var valorMaterial = 10.95;
        var material = new Material("teste", "teste", TipoMaterial.Cimento, valorMaterial, quantidadeMaterial);
        var operacao = _fixture.Create<EntradaSaida>();
        ObraMaterial? obraMaterial = null;
        
        var result = () =>
        {
            obraMaterial = new ObraMaterial(obra, material, quantidade, operacao);
        };

        obraMaterial.Should().BeNull();
        result.Should().Throw<ObraMaterialInvalidaException>();
    }
    
    [Fact]
    public void Construtor_ComOperacaoInvalida_DeveLancarExcecao()
    {
        var obra = _fixture.Create<Obra>();
        var quantidadeMaterial = 10;
        var valorMaterial = 10.95;
        var material = new Material("teste", "teste", TipoMaterial.Cimento, valorMaterial, quantidadeMaterial);
        var quantidade = _fixture.Create<int>();
        ObraMaterial? obraMaterial = null;
        
        var result = () =>
        {
            obraMaterial = new ObraMaterial(obra, material, quantidade, null);
        };

        obraMaterial.Should().BeNull();
        result.Should().Throw<ObraMaterialInvalidaException>();
    }
}