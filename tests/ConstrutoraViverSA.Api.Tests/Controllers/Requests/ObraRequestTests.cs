using System;
using System.Collections.Generic;
using AutoFixture;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace ConstrutoraViverSA.Api.Tests.Controllers.Requests;

public class ObraRequestTests
{
    private readonly Fixture _fixture = new Fixture();

    private static readonly List<long> LISTA_FUNCIONARIOS_VALIDA = new () { 1,2,3 };
    private static readonly List<long> LISTA_FUNCIONARIOS_INVALIDA = new ();
    private static readonly  Dictionary<long, int>? DICIONARIO_MATERIAIS_VALIDO = new (){ { 1, 1 }, { 2, 2 } };
    private static readonly Dictionary<long, int>? DICIONARIO_MATERIAIS_INVALIDO = new();

    [Fact]
    public void ValidarCricao_ComDadosValidos_DeveRetornarComoEsperado()
    {
        var request = _fixture.Build<ObraRequest>()
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();

        Action result = () => request.ValidarCriacao();

        result.Should().NotThrow<ErroValidacaoException>();
    }

    [Theory]
    [InlineData(null, "endereco", TipoObraEnum.Ambas, "descricao", 10.80, "2000-10-10", 1, false, false)]
    [InlineData(" ", null, TipoObraEnum.Ambas, "descricao", 10.80, "2000-10-10", 1, false, false)]
    [InlineData("nome", " ", TipoObraEnum.Ambas, "descricao", 10.80, "2000-10-10", 1, false, false)]
    [InlineData("nome", "endereco", null, "descricao", 10.80, "2000-10-10", 1, false, false)]
    [InlineData("nome", "endereco", TipoObraEnum.Ambas, null, 10.80, "2000-10-10", 1, false, false)]
    [InlineData("nome", "endereco", TipoObraEnum.Ambas, " ", 10.80, "2000-10-10", 1, false, false)]
    [InlineData("nome", "endereco", TipoObraEnum.Ambas, "descricao", 0, "2000-10-10", 1, false, false)]
    [InlineData("nome", "endereco", TipoObraEnum.Ambas, "descricao", -1, "2000-10-10", 1, false, false)]
    [InlineData("nome", "endereco", TipoObraEnum.Ambas, "descricao", null, "2000-10-10", 1, false, false)]
    [InlineData("nome", "endereco", TipoObraEnum.Ambas, "descricao", 10.80, null, 1, false, false)]
    [InlineData("nome", "endereco", TipoObraEnum.Ambas, "descricao", 10.80, "2000-10-10", 0, false, false)]
    [InlineData("nome", "endereco", TipoObraEnum.Ambas, "descricao", 10.80, "2000-10-10", 1, true, false)]
    [InlineData("nome", "endereco", TipoObraEnum.Ambas, "descricao", 10.80, "2000-10-10", 1, false, true)]
    public void ValidarCricao_ComDadosInvalidos_DeveRetornarComoEsperado(string nome, string endereco, TipoObraEnum? tipoObra, string descricao, double? valor, string prazoConclusao, long? orcamentoId, bool comFuncionarios, bool comMateriais)
    {
        var request = new ObraRequest()
        {
            Nome = nome,
            Endereco = endereco,
            Descricao = descricao,
            TipoObra = tipoObra,
            PrazoConclusao = prazoConclusao is not null ? DateTime.Parse(prazoConclusao) : null,
            OrcamentoId = orcamentoId,
            Funcionarios = comFuncionarios ? LISTA_FUNCIONARIOS_INVALIDA : null,
            Materiais = comMateriais ? DICIONARIO_MATERIAIS_INVALIDO : null
        };
        
        var result = () => request.ValidarCriacao();

        result.Should().Throw<ErroValidacaoException>();
    }
    
    [Fact]
    public void ValidarEdicao_ComDadosValidos_DeveRetornarComoEsperado(){
        var request = _fixture.Build<ObraRequest>()
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();

        Action result = () => request.ValidarEdicao();

        result.Should().NotThrow<ErroValidacaoException>();
    }
    
    [Theory]
    [InlineData("nome", " ", TipoObraEnum.Ambas, "descricao", 10.80, "2000-10-10", 1, false, false)]
    [InlineData("nome", "endereco", TipoObraEnum.Ambas, " ", 10.80, "2000-10-10", 1, false, false)]
    [InlineData("nome", "endereco", TipoObraEnum.Ambas, "descricao", 0, "2000-10-10", 1, false, false)]
    [InlineData("nome", "endereco", TipoObraEnum.Ambas, "descricao", -1, "2000-10-10", 1, false, false)]
    [InlineData("nome", "endereco", TipoObraEnum.Ambas, "descricao", 10.80, "2000-10-10", 0, false, false)]
    [InlineData("nome", "endereco", TipoObraEnum.Ambas, "descricao", 10.80, "2000-10-10", 1, true, false)]
    [InlineData("nome", "endereco", TipoObraEnum.Ambas, "descricao", 10.80, "2000-10-10", 1, false, true)]
    public void ValidarEdicao_ComDadosInvalidos_DeveRetornarComoEsperado(string nome, string endereco, TipoObraEnum? tipoObra, string descricao, double? valor, string prazoConclusao, long? orcamentoId, bool comFuncionarios, bool comMateriais)
    {
        var request = new ObraRequest()
        {
            Nome = nome,
            Endereco = endereco,
            Descricao = descricao,
            TipoObra = tipoObra,
            PrazoConclusao = prazoConclusao is not null ? DateTime.Parse(prazoConclusao) : null,
            OrcamentoId = orcamentoId,
            Funcionarios = comFuncionarios ? LISTA_FUNCIONARIOS_INVALIDA : null,
            Materiais = comMateriais ? DICIONARIO_MATERIAIS_INVALIDO : null
        };
        
        var result = () => request.ValidarEdicao();

        result.Should().Throw<ErroValidacaoException>();
    }
}