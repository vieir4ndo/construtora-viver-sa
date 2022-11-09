using System;
using AutoFixture;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace ConstrutoraViverSA.Api.Tests.Controllers.Requests;

public class EntradaSaidaMaterialRequestTests
{
    private readonly Fixture _fixture = new Fixture();

    [Fact]
    public void Validar_ComDadosValidos_DeveRetornarComoEsperado()
    {
        var request = _fixture.Create<EntradaSaidaMaterialRequest>();
        
        Action result = () => request.Validar();

        result.Should().NotThrow<ErroValidacaoException>();
    }
    
    [Theory]
    [InlineData(EntradaSaida.Entrada, null)]
    [InlineData(EntradaSaida.Saida, null)]
    [InlineData(null, 1)]
    public void Validar_ComDadosInvalidos_DeveRetornarComoEsperado(EntradaSaida? operacao, int? quantidade)
    {
        var request = new EntradaSaidaMaterialRequest()
        {
            Operacao = operacao,
            Quantidade = quantidade
        };
        
        Action result = () => request.Validar();

        result.Should().Throw<ErroValidacaoException>();
    }
}