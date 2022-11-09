#nullable enable
using System;
using System.Collections.Generic;
using AutoFixture;
using ConstrutoraViverSA.Api.Controllers.Responses;
using ConstrutoraViverSA.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace ConstrutoraViverSA.Api.Tests.Controllers.Responses;

public class ResponseApiTests
{
    private readonly Fixture _fixture = new Fixture();
    
    [Theory]
    [InlineData(true, null, null)]
    [InlineData(false, null, null)]
    [InlineData(true, null, "mensagem")]
    [InlineData(false, null, "mensagem")]
    public void Construtor_ComDadosValidos_DeveRetornarComoEsperado(bool sucesso, List<object>? dados, string mensagens)
    {
        var result = new ResponseApi<object>(sucesso, dados, mensagens);

        result.Should().NotBeNull();
        result.Sucesso.Should().Be(sucesso);
        result.Dados.Should().BeEquivalentTo(dados);
        result.Mensagens.Should().Be(mensagens);
    }
    
    [Fact]
    public void Construtor_ComDadosInvalidos_DeveRetornarComoEsperado()
    {
        Action result = () => new ResponseApi<object>(null, null, null);

        result.Should().Throw<ResponseApiInvalidoException>();
    }
}