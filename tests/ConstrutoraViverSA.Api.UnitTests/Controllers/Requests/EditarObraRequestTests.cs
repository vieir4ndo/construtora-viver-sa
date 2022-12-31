using System;
using System.Collections.Generic;
using AutoFixture;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace ConstrutoraViverSA.Api.UnitTests.Controllers.Requests;

public class EditarObraRequestTests
{
    private readonly Fixture _fixture = new Fixture();


    [Fact]
    public void ValidarEdicao_ComDadosValidos_DeveRetornarComoEsperado(){
        var request = _fixture.Build<EditarObraRequest>()
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();

        var result = () => request.ValidarEdicao();

        result.Should().NotThrow<ErroValidacaoException>();
    }
    
    [Theory]
    [InlineData(" ", "endereco", TipoObra.Ambas, "descricao", 10.80, "2000-10-10", 1)]
    [InlineData("nome", " ", TipoObra.Ambas, "descricao", 10.80, "2000-10-10", 1)]
    [InlineData("nome", "endereco", null, " ", 10.80, "2000-10-10", 1)]
    [InlineData("nome", "endereco", TipoObra.Ambas, " ", 10.80, "2000-10-10", 1)]
    [InlineData("nome", "endereco", TipoObra.Ambas, "descricao", 0, "2000-10-10", 1)]
    [InlineData("nome", "endereco", TipoObra.Ambas, "descricao", -1, "2000-10-10", 1)]
    [InlineData("nome", "endereco", TipoObra.Ambas, "descricao", 10.80, "2000-10-10", 0)]
    [InlineData("nome", "endereco", TipoObra.Ambas, "descricao", 10.80, "2000-10-10", -1)]
    public void ValidarEdicao_ComDadosInvalidos_DeveRetornarComoEsperado(string nome, string endereco, TipoObra? tipoObra, string descricao, double? valor, string prazoConclusao, long? orcamentoId)
    {
        var request = new EditarObraRequest()
        {
            Nome = nome,
            Endereco = endereco,
            Descricao = descricao,
            TipoObra = tipoObra,
            PrazoConclusao = prazoConclusao is not null ? DateTime.Parse(prazoConclusao) : null,
            OrcamentoId = orcamentoId,
            Valor = valor
        };
        
        var result = () => request.ValidarEdicao();

        result.Should().Throw<ErroValidacaoException>();
    }
}