using AutoFixture;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace ConstrutoraViverSA.Domain.Tests;

public class EstoqueTests
{
    private readonly Fixture _fixture = new Fixture();
    
    [Fact]
    public void Construtor_ComDadosValidos_DeveConstruirCorretamente()
    {
        var material = _fixture.Create<Material>();
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

        Action result = () => new Estoque(null, operacao, quantidade);

        result.Should().Throw<EstoqueInvalidoException>();
    }
    
    [Fact]
    
    public void Construtor_ComOperacaoInvalida_DeveLancarExcecao()
    {
        var quantidade = _fixture.Create<int>();
        var material = _fixture.Create<Material>();

        Action result = () => new Estoque(material, null, quantidade);

        result.Should().Throw<EstoqueInvalidoException>();
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData(-1)]
    [InlineData(0)]
    
    public void Construtor_ComQuantidadeInvalida_DeveLancarExcecao(int? quantidade)
    {
        var operacao = _fixture.Create<EntradaSaida>();
        var material = _fixture.Create<Material>();

        Action result = () => new Estoque(material, operacao, quantidade);

        result.Should().Throw<EstoqueInvalidoException>();
    }
}