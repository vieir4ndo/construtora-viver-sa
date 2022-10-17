using System;
using AutoFixture;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Api.Controllers.Validators;
using ConstrutoraViverSA.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace ConstrutoraViverSA.Api.Tests.Controllers.Validators;

public class EditarOrcamentoValidatorTests
{
    private readonly EditarOrcamentoValidator _validator = new EditarOrcamentoValidator();
    private readonly Fixture _fixture = new Fixture();
    
    [Theory]
    [InlineData("123123")]
    [InlineData("descricao")]
    [InlineData(null)]
    public void EditarOrcamentoValidator_DadoUmDescricaoValido_NaoDeveRetornarErros(string descricao)
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
    public void EditarOrcamentoValidator_DadoUmDescricaoInvalido_DeveRetornarErros(string descricao)
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
    [InlineData(null)]
    public void EditarOrcamentoValidator_DadoUmEnderecoValido_NaoDeveRetornarErros(string endereco)
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
    public void EditarOrcamentoValidator_DadoUmEnderecoInvalido_DeveRetornarErros(string endereco)
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
    [InlineData(TipoObraEnum.Ambas)]
    [InlineData(TipoObraEnum.Comercial)]
    [InlineData(TipoObraEnum.Residencial)]
    [InlineData(null)]
    public void EditarOrcamentoValidator_DadoUmTipoValido_NaoDeveRetornarErros(TipoObraEnum? tipoObra)
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
    public void EditarOrcamentoValidator_DadoUmTipoInvalido_DeveRetornarErros(int? tipoObra)
    {
        var request = _fixture.Build<OrcamentoRequest>()
            .With(x => x.TipoObra, tipoObra is null ? null : (TipoObraEnum)tipoObra)
            .With(x => x.DataEmissao, DateTime.Today)
            .With(x => x.DataValidade, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData(10.8)]
    [InlineData(1)]
    [InlineData(null)]
    public void EditarOrcamentoValidator_DadoUmValorValido_NaoDeveRetornarErros(double? valor)
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
    public void EditarOrcamentoValidator_DadoUmValorInvalido_DeveRetornarErros(double? valor)
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
    public void EditarOrcamentoValidator_DadoUmaDataEmissaoEDataValidadeEmIntervaloValido_NaoDeveRetornarErros()
    {
        var request = _fixture.Build<OrcamentoRequest>()
            .With(x => x.DataEmissao, DateTime.Today)
            .With(x => x.DataValidade, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void EditarOrcamentoValidator_DadoUmaDataEmissaoEDataValidadeEmIntervaloOInvalido_DeveRetornarErros()
    {
        var request = _fixture.Build<OrcamentoRequest>()
            .With(x => x.DataEmissao, DateTime.Today.AddDays(1))
            .With(x => x.DataValidade, DateTime.Today)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }

    [Fact]
    public void EditarOrcamentoValidator_DadoUmaDataEmissaoNulla_NaoDeveRetornarErros()
    {
        var request = _fixture.Build<OrcamentoRequest>()
            .Without(x => x.DataEmissao)
            .With(x => x.DataValidade, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void EditarOrcamentoValidator_DadoUmaDataValidadeNulla_NaoDeveRetornarErros()
    {
        var request = _fixture.Build<OrcamentoRequest>()
            .Without(x => x.DataValidade)
            .With(x => x.DataEmissao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
}