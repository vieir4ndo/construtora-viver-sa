using AutoFixture;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace ConstrutoraViverSA.Domain.Tests;

public class MaterialTests
{
    private readonly Fixture _fixture = new Fixture();

    [Fact]
    public void Construtor_ComDadosValidosEQuantidadeIgualAZero_DeveConstruirCorretamente()
    {
        var nome = _fixture.Create<string>();
        var descricao = _fixture.Create<string>();
        var tipo = _fixture.Create<TipoMaterialEnum>();
        var valor = _fixture.Create<double>();
        var quantidade = 0;

        var result = new Material(nome, descricao, tipo, valor, quantidade);

        result.Should().NotBeNull();
        result.Nome.Should().Be(nome);
        result.Descricao.Should().Be(descricao);
        result.Tipo.Should().Be(tipo);
        result.Valor.Should().Be(valor);
        result.Quantidade.Should().Be(quantidade);
        result.Estoque.Should().BeEmpty();
        result.ObraMateriais.Should().BeEmpty();
    }
    
    [Fact]
    public void Construtor_ComDadosValidosEQuantidadeMaiorQueZero_DeveConstruirCorretamente()
    {
        var nome = _fixture.Create<string>();
        var descricao = _fixture.Create<string>();
        var tipo = _fixture.Create<TipoMaterialEnum>();
        var valor = _fixture.Create<double>();
        var quantidade = 10;

        var result = new Material(nome, descricao, tipo, valor, quantidade);

        result.Should().NotBeNull();
        result.Nome.Should().Be(nome);
        result.Descricao.Should().Be(descricao);
        result.Tipo.Should().Be(tipo);
        result.Valor.Should().Be(valor);
        result.Quantidade.Should().Be(quantidade);
        result.Estoque.Count.Should().Be(1);
        result.Estoque.First().Quantidade.Should().Be(quantidade);
        result.Estoque.Count.Should().Be(1);
        result.ObraMateriais.Should().BeEmpty();
    }
    
    [Fact]
    public void Construtor_ComNomeInvalido_DeveLancarExcecao()
    {
        var descricao = _fixture.Create<string>();
        var tipo = _fixture.Create<TipoMaterialEnum>();
        var valor = _fixture.Create<double>();
        var quantidade = 0;

        Action result = () => new Material(null, descricao, tipo, valor, quantidade);

        result.Should().Throw<MaterialInvalidoException>();
    }
    
    [Fact]
    public void Construtor_ComDescricaoInvalido_DeveLancarExcecao()
    {
        var nome = _fixture.Create<string>();
        var tipo = _fixture.Create<TipoMaterialEnum>();
        var valor = _fixture.Create<double>();
        var quantidade = 0;

        Action result = () => new Material(nome, null, tipo, valor, quantidade);

        result.Should().Throw<MaterialInvalidoException>();
    }
    
    [Fact]
    public void Construtor_ComTipoInvalido_DeveLancarExcecao()
    {
        var nome = _fixture.Create<string>();
        var descricao = _fixture.Create<string>();
        var valor = _fixture.Create<double>();
        var quantidade = 0;

        Action result = () => new Material(nome, descricao, null, valor, quantidade);

        result.Should().Throw<MaterialInvalidoException>();
    }
    
    [Fact]
    public void Construtor_ComValorInvalido_DeveLancarExcecao()
    {
        var nome = _fixture.Create<string>();
        var descricao = _fixture.Create<string>();
        var tipo = _fixture.Create<TipoMaterialEnum>();
        var quantidade = 0;

        Action result = () => new Material(nome, descricao, tipo, null, quantidade);

        result.Should().Throw<MaterialInvalidoException>();
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData(-1)]
    public void Construtor_ComQuantidadeInvalido_DeveLancarExcecao(int? quantidade)
    {
        var nome = _fixture.Create<string>();
        var descricao = _fixture.Create<string>();
        var tipo = _fixture.Create<TipoMaterialEnum>();
        var valor = _fixture.Create<double>();

        Action result = () => new Material(nome, descricao, tipo, valor, quantidade);

        result.Should().Throw<MaterialInvalidoException>();
    }
    
    [Fact]
    public void SetNome_ComDadosValidos_DeveRealizarAlteracao()
    {
        var material = _fixture.Create<Material>();
        var nomeNovo = _fixture.Create<string>();
        
        material.SetNome(nomeNovo);

        material.Nome.Should().Be(nomeNovo);
    }
    
    [Fact]
    public void SetNome_ComDadosInvalidos_NaoDeveRealizarAlteracao()
    {
        var material = _fixture.Create<Material>();
        var nomeAntigo = material.Nome;
        
        material.SetNome(null);

        material.Nome.Should().Be(nomeAntigo);
    }
    
    [Fact]
    public void SetDescricao_ComDadosValidos_DeveRealizarAlteracao()
    {
        var material = _fixture.Create<Material>();
        var descricaoNovo = _fixture.Create<string>();
        
        material.SetDescricao(descricaoNovo);

        material.Descricao.Should().Be(descricaoNovo);
    }
    
    [Fact]
    public void SetDescricao_ComDadosInvalidos_NaoDeveRealizarAlteracao()
    {
        var material = _fixture.Create<Material>();
        var descricaoAntiga = material.Descricao;
        
        material.SetDescricao(null);

        material.Descricao.Should().Be(descricaoAntiga);
    }
    
    [Fact]
    public void SetTipo_ComDadosValidos_DeveRealizarAlteracao()
    {
        var material = _fixture.Create<Material>();
        var tipoNovo = _fixture.Create<TipoMaterialEnum>();
        
        material.SetTipo(tipoNovo);

        material.Tipo.Should().Be(tipoNovo);
    }
    
    [Fact]
    public void SetTipo_ComDadosInvalidos_NaoDeveRealizarAlteracao()
    {
        var material = _fixture.Create<Material>();
        var tipoAntigo = material.Tipo;
        
        material.SetTipo(null);

        material.Tipo.Should().Be(tipoAntigo);
    }
    
    [Fact]
    public void SetValor_ComDadosValidos_DeveRealizarAlteracao()
    {
        var material = _fixture.Create<Material>();
        var valorNovo = _fixture.Create<double>();
        
        material.SetValor(valorNovo);

        material.Valor.Should().Be(valorNovo);
    }
    
    [Fact]
    public void SetValor_ComDadosInvalidos_NaoDeveRealizarAlteracao()
    {
        var material = _fixture.Create<Material>();
        var valorAntigo = material.Valor;
        
        material.SetValor(null);

        material.Valor.Should().Be(valorAntigo);
    }

    [Fact]
    public void MovimentarEstoque_ComDadosValidos_DeveRetornarComoEsperado()
    {
        var material = new Material("teste", "teste", TipoMaterialEnum.Cimento, 10.80, 1);
        var quantidade = 10;
        var operacao = EntradaSaidaEnum.Entrada;
        
        material.MovimentarEstoque(operacao, quantidade);

        material.Estoque.Count.Should().Be(2);
        material.Estoque.Last().Quantidade.Should().Be(quantidade);
        material.Estoque.Last().Operacao.Should().Be(operacao);
    }
    
    [Fact]
    public void MovimentarEstoque_ComQuantidadeInvalida_DeveLancarExcecao()
    {
        var material = new Material("teste", "teste", TipoMaterialEnum.Cimento, 10.80, 1);
        var quantidade = -10;
        var operacao = EntradaSaidaEnum.Entrada;
        
        Action result = () => material.MovimentarEstoque(operacao, quantidade);

        result.Should().Throw<OperacaoInvalidaException>();
    }
    
    [Fact]
    public void MovimentarEstoque_SaidaComMaterialSemEstoqueSuficiente_DeveLancarExcecao()
    {
        var material = new Material("teste", "teste", TipoMaterialEnum.Cimento, 10.80, 1);
        var quantidade = 10;
        var operacao = EntradaSaidaEnum.Saida;
        
        Action result = () => material.MovimentarEstoque(operacao, quantidade);

        result.Should().Throw<OperacaoInvalidaException>();
    }
}