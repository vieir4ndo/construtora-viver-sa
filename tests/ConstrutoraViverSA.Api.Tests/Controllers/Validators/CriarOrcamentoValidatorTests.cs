using System;
using AutoFixture;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Api.Controllers.Validators;
using ConstrutoraViverSA.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace ConstrutoraViverSA.Api.Tests.Controllers.Validators;

public class CriarOrcamentoValidatorTests
{
    private readonly CriarOrcamentoValidator _validator = new CriarOrcamentoValidator();
    private readonly Fixture _fixture = new Fixture();
    
    [Theory]
    [InlineData("123123")]
    [InlineData("descricao")]
    public void CriarOrcamentoValidator_DadoUmDescricaoValido_NaoDeveRetornarErros(string descricao)
    {
        var request = _fixture.Build<OrcamentoRequest>()
            .With(x => x.Descricao, descricao)
            .With(x => x.DataEmissao, DateTime.Today)
            .With(x => x.DataValidade, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(" ")]
    [InlineData(null)]
    public void CriarOrcamentoValidator_DadoUmDescricaoInvalido_DeveRetornarErros(string descricao)
    {
        var request = _fixture.Build<OrcamentoRequest>()
            .With(x => x.Descricao, descricao)
            .With(x => x.DataEmissao, DateTime.Today)
            .With(x => x.DataValidade, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData("Endereco")]
    [InlineData("Rua do Saber")]
    public void CriarOrcamentoValidator_DadoUmEnderecoValido_NaoDeveRetornarErros(string endereco)
    {
        var request = _fixture.Build<OrcamentoRequest>()
            .With(x => x.Endereco, endereco)
            .With(x => x.DataEmissao, DateTime.Today)
            .With(x => x.DataValidade, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(" ")]
    [InlineData(null)]
    public void CriarOrcamentoValidator_DadoUmEnderecoInvalido_DeveRetornarErros(string endereco)
    {
        var request = _fixture.Build<OrcamentoRequest>()
            .With(x => x.Endereco, endereco)
            .With(x => x.DataEmissao, DateTime.Today)
            .With(x => x.DataValidade, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData(TipoObra.Ambas)]
    [InlineData(TipoObra.Comercial)]
    [InlineData(TipoObra.Residencial)]
    public void CriarOrcamentoValidator_DadoUmTipoValido_NaoDeveRetornarErros(TipoObra tipoObra)
    {
        var request = _fixture.Build<OrcamentoRequest>()
            .With(x => x.TipoObra, tipoObra)
            .With(x => x.DataEmissao, DateTime.Today)
            .With(x => x.DataValidade, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(1455767)]
    [InlineData(null)]
    public void CriarOrcamentoValidator_DadoUmTipoInvalido_DeveRetornarErros(int? tipoObra)
    {
        var request = _fixture.Build<OrcamentoRequest>()
            .With(x => x.TipoObra, tipoObra is null ? null : (TipoObra)tipoObra)
            .With(x => x.DataEmissao, DateTime.Today)
            .With(x => x.DataValidade, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData(10.8)]
    [InlineData(1)]
    public void CriarOrcamentoValidator_DadoUmValorValido_NaoDeveRetornarErros(double valor)
    {
        var request = _fixture.Build<OrcamentoRequest>()
            .With(x => x.Valor, valor)
            .With(x => x.DataEmissao, DateTime.Today)
            .With(x => x.DataValidade, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(null)]
    public void CriarOrcamentoValidator_DadoUmValorInvalido_DeveRetornarErros(double? valor)
    {
        var request = _fixture.Build<OrcamentoRequest>()
            .With(x => x.Valor, valor)
            .With(x => x.DataEmissao, DateTime.Today)
            .With(x => x.DataValidade, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void CriarOrcamentoValidator_DadoUmaDataEmissaoEDataValidadeEmIntervaloValido_NaoDeveRetornarErros()
    {
        var request = _fixture.Build<OrcamentoRequest>()
            .With(x => x.DataEmissao, DateTime.Today)
            .With(x => x.DataValidade, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void CriarOrcamentoValidator_DadoUmaDataEmissaoEDataValidadeEmIntervaloOInvalido_DeveRetornarErros()
    {
        var request = _fixture.Build<OrcamentoRequest>()
            .With(x => x.DataEmissao, DateTime.Today.AddDays(1))
            .With(x => x.DataValidade, DateTime.Today)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }

    [Fact]
    public void CriarOrcamentoValidator_DadoUmaDataEmissaoNulla_DeveRetornarErros()
    {
        var request = _fixture.Build<OrcamentoRequest>()
            .Without(x => x.DataEmissao)
            .With(x => x.DataValidade, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void CriarOrcamentoValidator_DadoUmaDataValidadeNulla_DeveRetornarErros()
    {
        var request = _fixture.Build<OrcamentoRequest>()
            .Without(x => x.DataValidade)
            .With(x => x.DataEmissao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
}