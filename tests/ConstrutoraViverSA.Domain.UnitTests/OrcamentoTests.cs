using AutoFixture;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace ConstrutoraViverSA.Domain.Tests;

public class OrcamentoTests
{
    private readonly Fixture _fixture = new Fixture();

    [Fact]
    public void Construtor_ComDadosValidos_DeveConstruirCorretamente()
    {
        var descricao = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var tipoObra = _fixture.Create<TipoObra>();
        var dataEmissao = DateTime.Today;
        var dataValidade = DateTime.Today.AddDays(1);
        var valor = _fixture.Create<double>();

        var result = new Orcamento(descricao, endereco, tipoObra, dataEmissao, dataValidade, valor);

        result.Should().NotBeNull();
        result.Descricao.Should().Be(descricao);
        result.Endereco.Should().Be(endereco);
        result.TipoObra.Should().Be(tipoObra);
        result.DataEmissao.Should().Be(dataEmissao);
        result.DataValidade.Should().Be(dataValidade);
        result.Valor.Should().Be(valor);
    }
    
    [Fact]
    public void Construtor_ComDescricaoInvalida_DeveLancarExcecao()
    {
        var endereco = _fixture.Create<string>();
        var tipoObra = _fixture.Create<TipoObra>();
        var dataEmissao = DateTime.Today;
        var dataValidade = DateTime.Today.AddDays(1);
        var valor = _fixture.Create<double>();

        Action result = () => new Orcamento(null, endereco, tipoObra, dataEmissao, dataValidade, valor);

        result.Should().Throw<OrcamentoInvalidoException>();
    }
    
    [Fact]
    public void Construtor_ComEnderecoInvalido_DeveLancarExcecao()
    {
        var descricao = _fixture.Create<string>();
        var tipoObra = _fixture.Create<TipoObra>();
        var dataEmissao = DateTime.Today;
        var dataValidade = DateTime.Today.AddDays(1);
        var valor = _fixture.Create<double>();

        Action result = () => new Orcamento(descricao, null, tipoObra, dataEmissao, dataValidade, valor);

        result.Should().Throw<OrcamentoInvalidoException>();
    }
    
    [Fact]
    public void Construtor_ComTipoObraInvalido_DeveLancarExcecao()
    {
        var descricao = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var dataEmissao = DateTime.Today;
        var dataValidade = DateTime.Today.AddDays(1);
        var valor = _fixture.Create<double>();

        Action result = () => new Orcamento(descricao, endereco, null, dataEmissao, dataValidade, valor);

        result.Should().Throw<OrcamentoInvalidoException>();
    }
    
    [Fact]
    public void Construtor_ComDataEmissaoNull_DeveLancarExcecao()
    {
        var descricao = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var tipoObra = _fixture.Create<TipoObra>();
        var dataValidade = DateTime.Today.AddDays(1);
        var valor = _fixture.Create<double>();

        Action result = () => new Orcamento(descricao, endereco, tipoObra, null, dataValidade, valor);

        result.Should().Throw<OrcamentoInvalidoException>();
    }
    
    [Fact]
    public void Construtor_ComDataEmissaoInvalida_DeveLancarExcecao()
    {
        var descricao = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var tipoObra = _fixture.Create<TipoObra>();
        var dataValidade = DateTime.Today.AddDays(1);
        var valor = _fixture.Create<double>();

        Action result = () => new Orcamento(descricao, endereco, tipoObra, DateTime.MinValue, dataValidade, valor);

        result.Should().Throw<OrcamentoInvalidoException>();
    }
    
    [Fact]
    public void Construtor_ComDataValidadeNull_DeveLancarExcecao()
    {
        var descricao = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var tipoObra = _fixture.Create<TipoObra>();
        var dataEmissao = DateTime.Today;
        var valor = _fixture.Create<double>();

        Action result = () => new Orcamento(descricao, endereco, tipoObra, dataEmissao, null, valor);

        result.Should().Throw<OrcamentoInvalidoException>();
    }
    
    [Fact]
    public void Construtor_ComDataValidadeInvalida_DeveLancarExcecao()
    {
        var descricao = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var tipoObra = _fixture.Create<TipoObra>();
        var dataEmissao = DateTime.Today;
        var valor = _fixture.Create<double>();

        Action result = () => new Orcamento(descricao, endereco, tipoObra, dataEmissao, DateTime.MinValue, valor);

        result.Should().Throw<OrcamentoInvalidoException>();
    }
    
    [Fact]
    public void Construtor_ComIntervaloDataEmissaoEDataValidadeInvalido_DeveLancarExcecao()
    {
        var descricao = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var tipoObra = _fixture.Create<TipoObra>();
        var dataEmissao = DateTime.Today.AddDays(1);
        var dataValidade = DateTime.Today;
        var valor = _fixture.Create<double>();

        Action result = () => new Orcamento(descricao, endereco, tipoObra, dataEmissao, dataValidade, valor);

        result.Should().Throw<OrcamentoInvalidoException>();
    }
    
    [Fact]
    public void Construtor_ComValorInvalido_DeveLancarExcecao()
    {
        var descricao = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var tipoObra = _fixture.Create<TipoObra>();
        var dataEmissao = DateTime.Today;
        var dataValidade = DateTime.Today.AddDays(1);

        Action result = () => new Orcamento(descricao, endereco, tipoObra, dataEmissao, dataValidade, null);

        result.Should().Throw<OrcamentoInvalidoException>();
    }
    
    [Fact]
    public void SetDescricao_ComDadosValidos_DeveRealizarAlteracao()
    {
        var orcamento = _fixture.Create<Orcamento>();
        var descricaoNova = _fixture.Create<string>();
        
        orcamento.SetDescricao(descricaoNova);

        orcamento.Descricao.Should().Be(descricaoNova);
    }
    
    [Fact]
    public void SetDescricao_ComDadosInvalidos_NaoDeveRealizarAlteracao()
    {
        var orcamento = _fixture.Create<Orcamento>();
        var descricaoAntiga = orcamento.Descricao;
        
        orcamento.SetDescricao(null);

        orcamento.Descricao.Should().Be(descricaoAntiga);
    }
    
    [Fact]
    public void SetEndereco_ComDadosValidos_DeveRealizarAlteracao()
    {
        var orcamento = _fixture.Create<Orcamento>();
        var enderecoNova = _fixture.Create<string>();
        
        orcamento.SetEndereco(enderecoNova);

        orcamento.Endereco.Should().Be(enderecoNova);
    }
    
    [Fact]
    public void SetEndereco_ComDadosInvalidos_NaoDeveRealizarAlteracao()
    {
        var orcamento = _fixture.Create<Orcamento>();
        var enderecoAntiga = orcamento.Endereco;
        
        orcamento.SetEndereco(null);

        orcamento.Endereco.Should().Be(enderecoAntiga);
    }
    
    [Fact]
    public void SetTipoObra_ComDadosValidos_DeveRealizarAlteracao()
    {
        var orcamento = _fixture.Create<Orcamento>();
        var tipoObraNovo = _fixture.Create<TipoObra>();
        
        orcamento.SetTipoObra(tipoObraNovo);

        orcamento.TipoObra.Should().Be(tipoObraNovo);
    }
    
    [Fact]
    public void SetTipoObra_ComDadosInvalidos_NaoDeveRealizarAlteracao()
    {
        var orcamento = _fixture.Create<Orcamento>();
        var tipoObraAntigo = orcamento.TipoObra;
        
        orcamento.SetTipoObra(null);

        orcamento.TipoObra.Should().Be(tipoObraAntigo);
    }
    
    [Fact]
    public void SetDataEmissao_ComDadosValidos_DeveRealizarAlteracao()
    {
        var descricao = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var tipoObra = _fixture.Create<TipoObra>();
        var dataEmissao = DateTime.Today;
        var dataValidade = DateTime.Today.AddDays(2);
        var valor = _fixture.Create<double>();

        var orcamento = new Orcamento(descricao, endereco, tipoObra, dataEmissao, dataValidade, valor);
        var dataEmissaoNova = dataEmissao.AddDays(1);
        
        orcamento.SetDataEmissao(dataEmissaoNova);

        orcamento.DataEmissao.Should().Be(dataEmissaoNova);
    }
    
    [Fact]
    public void SetDataEmissao_ComDadoNull_NaoDeveRealizarAlteracao()
    {
        var orcamento = _fixture.Create<Orcamento>();
        var dataEmissaoAntigo = orcamento.DataEmissao;
        
        orcamento.SetDataEmissao(null);

        orcamento.DataEmissao.Should().Be(dataEmissaoAntigo);
    }
    
    [Fact]
    public void SetDataEmissao_ComDadosInvalidos_NaoDeveRealizarAlteracao()
    {
        var descricao = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var tipoObra = _fixture.Create<TipoObra>();
        var dataEmissao = DateTime.Today;
        var dataValidade = DateTime.Today.AddDays(1);
        var valor = _fixture.Create<double>();

        var orcamento = new Orcamento(descricao, endereco, tipoObra, dataEmissao, dataValidade, valor);
        
        orcamento.SetDataEmissao(dataValidade.AddDays(+3));

        orcamento.DataEmissao.Should().Be(dataEmissao);
    }
    
    [Fact]
    public void SetDataValidade_ComDadosValidos_DeveRealizarAlteracao()
    {
        var descricao = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var tipoObra = _fixture.Create<TipoObra>();
        var dataEmissao = DateTime.Today;
        var dataValidade = DateTime.Today.AddDays(2);
        var valor = _fixture.Create<double>();
        var dataValidadeNovo = dataValidade.AddDays(1);
        var orcamento = new Orcamento(descricao, endereco, tipoObra, dataEmissao, dataValidade, valor);
        
        orcamento.SetDataValidade(dataValidadeNovo);

        orcamento.DataValidade.Should().Be(dataValidadeNovo);
    }
    
    [Fact]
    public void SetDataValidade_ComDadoNull_NaoDeveRealizarAlteracao()
    {
        var orcamento = _fixture.Create<Orcamento>();
        var dataValidadeAntigo = orcamento.DataValidade;
        
        orcamento.SetDataValidade(null);

        orcamento.DataValidade.Should().Be(dataValidadeAntigo);
    }
    
    [Fact]
    public void SetDataValidade_ComDadosInvalidos_NaoDeveRealizarAlteracao()
    {
        var descricao = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var tipoObra = _fixture.Create<TipoObra>();
        var dataEmissao = DateTime.Today;
        var dataValidade = DateTime.Today.AddDays(2);
        var valor = _fixture.Create<double>();
        var orcamento = new Orcamento(descricao, endereco, tipoObra, dataEmissao, dataValidade, valor);
        
        orcamento.SetDataValidade(dataEmissao.AddDays(-1));

        orcamento.DataValidade.Should().Be(dataValidade);
    }
    
    [Fact]
    public void SetValor_ComDadosValidos_DeveRealizarAlteracao()
    {
        var orcamento = _fixture.Create<Orcamento>();
        var valorNovo = _fixture.Create<double>();
        
        orcamento.SetValor(valorNovo);

        orcamento.Valor.Should().Be(valorNovo);
    }
    
    [Fact]
    public void SetValor_ComDadosInvalidos_NaoDeveRealizarAlteracao()
    {
        var orcamento = _fixture.Create<Orcamento>();
        var valorAntigo = orcamento.Valor;
        
        orcamento.SetValor(null);

        orcamento.Valor.Should().Be(valorAntigo);
    }
}