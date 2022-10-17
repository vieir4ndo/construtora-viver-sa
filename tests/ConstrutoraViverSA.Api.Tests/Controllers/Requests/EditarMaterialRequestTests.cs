using System;
using AutoFixture;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace ConstrutoraViverSA.Api.Tests.Controllers.Requests;

public class EditarMaterialRequestTests
{
    private readonly Fixture _fixture = new Fixture();
    
    [Fact]
    public void ValidarEdicao_ComDadosValidos_DeveSeComportarComoEsperado()
    {
        var request = _fixture.Create<EditarMaterialRequest>();
        
        Action result = () => request.ValidarEdicao();

        result.Should().NotThrow<ErroValidacaoException>();
    }
    
    [Theory]
    [InlineData(" ", "descricao", TipoMaterialEnum.Cimento, 10.80)]
    [InlineData("nome", " ", TipoMaterialEnum.Cimento, 10.80)]
    [InlineData("012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789", " ", TipoMaterialEnum.Cimento, 10.80)]
    [InlineData("nome", "descricao", TipoMaterialEnum.Cimento, -1)]
    
    public void ValidarEdicao_ComDadosInvalidos_DeveSeComportarComoEsperado(string nome, string descricao, TipoMaterialEnum? tipo, double? valor)
    {
        var request = new EditarMaterialRequest()
        {
            Nome = nome,
            Descricao = descricao,
            Tipo = tipo,
            Valor = valor
        };

        
        Action result = () => request.ValidarEdicao();

        result.Should().Throw<ErroValidacaoException>();
    }
}