using AutoFixture;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;
using ConstrutoraViverSA.Domain.Tests.Stubs;
using FluentAssertions;
using Xunit;

namespace ConstrutoraViverSA.Domain.Tests;

public class EstoqueTests
{
    private readonly Fixture _fixture = new Fixture();
    
    [Fact]
    public void Construtor_ComDadosValidos_DeveConstruirCorretamente()
    {
        var material = MaterialStub.Valido(_fixture);
        var operacao = _fixture.Create<EntradaSaida>();
        var quantidade = _fixture.Create<int>();

        var result = new Estoque(material, operacao, quantidade);

        result.Should().NotBeNull();
        result.Material.Should().BeEquivalentTo(material);
        result.Operacao.Should().Be(operacao);
        result.Quantidade.Should().Be(quantidade);
        result.DataHora.Should().NotBe(null);
    }
    
    [Fact]
    
    public void Construtor_ComMaterialInvalido_DeveLancarExcecao()
    {
        var operacao = _fixture.Create<EntradaSaida>();
        var quantidade = _fixture.Create<int>();
        Estoque? estoque = null;

        var result = () =>
        {
            estoque = new Estoque(null, operacao, quantidade);
        };

        estoque.Should().BeNull();
        result.Should().Throw<EstoqueInvalidoException>();
    }
    
    [Fact]
    
    public void Construtor_ComOperacaoInvalida_DeveLancarExcecao()
    {
        var quantidade = _fixture.Create<int>();
        var material = MaterialStub.Valido(_fixture);
        Estoque? estoque = null;
        
        var result = () =>
        {
            estoque = new Estoque(material, null, quantidade);
        };

        estoque.Should().BeNull();
        result.Should().Throw<EstoqueInvalidoException>();
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData(-1)]
    [InlineData(0)]
    
    public void Construtor_ComQuantidadeInvalida_DeveLancarExcecao(int? quantidade)
    {
        var operacao = _fixture.Create<EntradaSaida>();
        var material = MaterialStub.Valido(_fixture);
        Estoque? estoque = null;
        
        var result = () =>
        {
            estoque = new Estoque(material, operacao, quantidade);
        };

        estoque.Should().BeNull();
        result.Should().Throw<EstoqueInvalidoException>();
    }
}