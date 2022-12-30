using System;
using AutoFixture;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace ConstrutoraViverSA.Api.UnitTests.Controllers.Requests;

public class MaterialRequestTests
{
    private readonly Fixture _fixture = new Fixture();
    
    [Fact]
    public void ValidarCriacao_ComDadosValidos_DeveSeComportarComoEsperado()
    {
        var request = _fixture.Create<MaterialRequest>();
        
        var result = () => request.ValidarCriacao();

        result.Should().NotThrow<ErroValidacaoException>();
    }
    
    [Theory]
    [InlineData(null, "descricao", TipoMaterial.Cimento, 10.80)]
    [InlineData("nome", null, TipoMaterial.Cimento, 10.80)]
    [InlineData("012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789", null, TipoMaterial.Cimento, 10.80)]
    [InlineData(" ", "descricao", TipoMaterial.Cimento, 10.80)]
    [InlineData("nome", " ", TipoMaterial.Cimento, 10.80)]
    [InlineData("nome", "descricao", null, 10.80)]
    [InlineData("nome", "descricao", TipoMaterial.Cimento, null)]
    
    public void ValidarCriacao_ComDadosInvalidos_DeveSeComportarComoEsperado(string nome, string descricao, TipoMaterial? tipo, double? valor)
    {
        var request = new MaterialRequest()
        {
            Nome = nome,
            Descricao = descricao,
            Tipo = tipo,
            Valor = valor
        };

        
        var result = () => request.ValidarCriacao();

        result.Should().Throw<ErroValidacaoException>();
    }
}