using System;
using AutoFixture;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace ConstrutoraViverSA.Api.Tests.Controllers.Requests;

public class MaterialRequestTests
{
    private readonly Fixture _fixture = new Fixture();
    
    [Fact]
    public void ValidarCriacao_ComDadosValidos_DeveSeComportarComoEsperado()
    {
        var request = _fixture.Create<MaterialRequest>();
        
        Action result = () => request.ValidarCriacao();

        result.Should().NotThrow<ErroValidacaoException>();
    }
    
    [Theory]
    [InlineData(null, "descricao", TipoMaterialEnum.Cimento, 10.80)]
    [InlineData("nome", null, TipoMaterialEnum.Cimento, 10.80)]
    [InlineData("012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789", null, TipoMaterialEnum.Cimento, 10.80)]
    [InlineData(" ", "descricao", TipoMaterialEnum.Cimento, 10.80)]
    [InlineData("nome", " ", TipoMaterialEnum.Cimento, 10.80)]
    [InlineData("nome", "descricao", null, 10.80)]
    [InlineData("nome", "descricao", TipoMaterialEnum.Cimento, null)]
    
    public void ValidarCriacao_ComDadosInvalidos_DeveSeComportarComoEsperado(string nome, string descricao, TipoMaterialEnum? tipo, double? valor)
    {
        var request = new MaterialRequest()
        {
            Nome = nome,
            Descricao = descricao,
            Tipo = tipo,
            Valor = valor
        };

        
        Action result = () => request.ValidarCriacao();

        result.Should().Throw<ErroValidacaoException>();
    }
}