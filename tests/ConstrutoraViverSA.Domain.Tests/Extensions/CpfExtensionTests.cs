using ConstrutoraViverSA.Domain.Extensions;
using FluentAssertions;
using Xunit;

namespace ConstrutoraViverSA.Domain.Tests.Extensions;

public class CpfExtensionTests
{
    [Theory]
    [InlineData("87359142032")]
    [InlineData("45554911064")]
    [InlineData("30314867066")]
    [InlineData("65689631000")]
    public void EhValido_ComDadosValidos_DeveRetornarVerdadeiro(string cpf)
    {
        var result = CpfExtension.EhValido(cpf);

        result.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("01747985263")]
    [InlineData("12212212234")]
    [InlineData("09901448878")]
    [InlineData("85240236559")]
    public void EhValido_ComDadosInvalidos_DeveRetornarFalso(string cpf)
    {
        var result = CpfExtension.EhValido(cpf);

        result.Should().BeFalse();
    }
}