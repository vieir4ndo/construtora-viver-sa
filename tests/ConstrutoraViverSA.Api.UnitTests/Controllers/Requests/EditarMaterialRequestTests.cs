using System;
using AutoFixture;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace ConstrutoraViverSA.Api.UnitTests.Controllers.Requests;

public class EditarMaterialRequestTests
{
    private readonly Fixture _fixture = new Fixture();
    
    [Fact]
    public void ValidarEdicao_ComDadosValidos_DeveSeComportarComoEsperado()
    {
        var request = _fixture.Create<EditarMaterialRequest>();
        
        var result = () => request.ValidarEdicao();

        result.Should().NotThrow<ErroValidacaoException>();
    }
    
    [Theory]
    [InlineData(" ", "descricao", TipoMaterial.Cimento, 10.80)]
    [InlineData("nome", " ", TipoMaterial.Cimento, 10.80)]
    [InlineData("012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789", " ", TipoMaterial.Cimento, 10.80)]
    [InlineData("nome", "descricao", TipoMaterial.Cimento, -1)]
    
    public void ValidarEdicao_ComDadosInvalidos_DeveSeComportarComoEsperado(string nome, string descricao, TipoMaterial? tipo, double? valor)
    {
        var request = new EditarMaterialRequest()
        {
            Nome = nome,
            Descricao = descricao,
            Tipo = tipo,
            Valor = valor
        };

        
        var result = () => request.ValidarEdicao();

        result.Should().Throw<ErroValidacaoException>();
    }
}