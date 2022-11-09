using System;
using AutoFixture;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace ConstrutoraViverSA.Api.Tests.Controllers.Requests;

public class OrcamentoRequestTests
{
    private readonly Fixture _fixture = new Fixture();

    [Fact]
    public void ValidarCriacao_ComDadosValidos_DeveRetornarComoEsperado()
    {
        var request = _fixture.Build<OrcamentoRequest>()
            .With(X => X.DataEmissao, DateTime.Today)
            .With(x => x.DataValidade, DateTime.Today.AddDays(1))
            .Create();
        
        var result = () => request.ValidarCriacao();

        result.Should().NotThrow<ErroValidacaoException>();
    }
    
    [Theory]
    [InlineData(" ", "endereco", TipoObra.Ambas, "2000-06-10", "2000-07-12", 10.80)]
    [InlineData(null, "endereco", TipoObra.Ambas, "2000-06-10", "2000-07-12", 10.80)]
    [InlineData("descricao", " ", TipoObra.Ambas, "2000-06-10", "2000-07-12", 10.80)]
    [InlineData("descricao", null, TipoObra.Ambas, "2000-06-10", "2000-07-12", 10.80)]
    [InlineData("descricao", "endereco", null, "2000-06-10", "2000-07-12", 10.80)]
    [InlineData("descricao", "endereco", TipoObra.Ambas, null, "2000-07-12", 10.80)]
    [InlineData("descricao", "endereco", TipoObra.Ambas, "2000-06-10", null, 10.80)]
    [InlineData("descricao", "endereco", TipoObra.Ambas, "2000-06-10", "2000-07-12", 0)]
    [InlineData("descricao", "endereco", TipoObra.Ambas, "2000-06-10", "2000-07-12", -1)]
    [InlineData("descricao", "endereco", TipoObra.Ambas, "2000-06-10", "2000-07-12", null)]
    public void ValidarCriacao_ComDadosInvalidos_DeveRetornarComoEsperado(string descricao, string endereco, TipoObra? tipoObra, string dataEmissao, string dataValidade, double? valor)
    {
        var request = new OrcamentoRequest()
        {
            Descricao = descricao,
            Endereco = endereco,
            TipoObra = tipoObra,
            DataEmissao = dataEmissao is not null ? DateTime.Parse(dataEmissao) : null,
            DataValidade = dataValidade is not null ? DateTime.Parse(dataValidade) : null,
            Valor = valor
        };
        
        var result = () => request.ValidarCriacao();

        result.Should().Throw<ErroValidacaoException>();
    }
    
    [Fact]
    public void ValidarEdicao_ComDadosValidos_DeveRetornarComoEsperado()
    {
        var request = _fixture.Build<OrcamentoRequest>()
            .With(X => X.DataEmissao, DateTime.Today)
            .With(x => x.DataValidade, DateTime.Today.AddDays(1))
            .Create();
        
        var result = () => request.ValidarEdicao();

        result.Should().NotThrow<ErroValidacaoException>();
    }
    
    [Theory]
    [InlineData(" ", "endereco", TipoObra.Ambas, "2000-06-10", "2000-07-12", 10.80)]
    [InlineData("descricao", " ", TipoObra.Ambas, "2000-06-10", "2000-07-12", 10.80)]
    [InlineData("descricao", "endereco", TipoObra.Ambas, "2000-06-10", "2000-07-12", 0)]
    [InlineData("descricao", "endereco", TipoObra.Ambas, "2000-06-10", "2000-07-12", -1)]
    public void ValidarEdicao_ComDadosInvalidos_DeveRetornarComoEsperado(string descricao, string endereco, TipoObra? tipoObra, string dataEmissao, string dataValidade, double? valor)
    {
        var request = new OrcamentoRequest()
        {
            Descricao = descricao,
            Endereco = endereco,
            TipoObra = tipoObra,
            DataEmissao = dataEmissao is not null ? DateTime.Parse(dataEmissao) : null,
            DataValidade = dataValidade is not null ? DateTime.Parse(dataValidade) : null,
            Valor = valor
        };
        
        var result = () => request.ValidarEdicao();

        result.Should().Throw<ErroValidacaoException>();
    }
}