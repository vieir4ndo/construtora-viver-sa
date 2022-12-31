using AutoFixture;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Api.Controllers.Validators;
using ConstrutoraViverSA.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace ConstrutoraViverSA.Api.UnitTests.Controllers.Validators;

public class EntradaSaidaMaterialValidatorTests
{
    private readonly EntradaSaidaMaterialValidator _validator = new ();
    private readonly Fixture _fixture = new Fixture();
    
    [Theory]
    [InlineData(EntradaSaida.Entrada)]
    [InlineData(EntradaSaida.Saida)]
    public void EntradaSaidaMaterialValidator_DadoUmaOperacaoValida_NaoDeveRetornarErros(EntradaSaida operacao)
    {
        var request = _fixture.Build<EntradaSaidaMaterialRequest>()
            .With(x => x.Operacao, operacao)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(1455767)]
    [InlineData(null)]
    public void EntradaSaidaMaterialValidator_DadoUmaOperacaoInvalida_DeveRetornarErros(int? operacao)
    {
        var request = _fixture.Build<EntradaSaidaMaterialRequest>()
            .With(x => x.Operacao, (operacao is null) ? null : (EntradaSaida)operacao)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData(10)]
    [InlineData(1)]
    public void EntradaSaidaMaterialValidator_DadoUmaQuantidadeValido_NaoDeveRetornarErros(int quantidade)
    {
        var request = _fixture.Build<EntradaSaidaMaterialRequest>()
            .With(x => x.Quantidade, quantidade)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(null)]
    public void EntradaSaidaMaterialValidator_DadoUmaQuantidadeInvalido_DeveRetornarErros(int? quantidade)
    {
        var request = _fixture.Build<EntradaSaidaMaterialRequest>()
            .With(x => x.Quantidade, quantidade)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
}