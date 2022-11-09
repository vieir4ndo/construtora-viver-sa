using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Api.Controllers.Validators;
using ConstrutoraViverSA.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace ConstrutoraViverSA.Api.Tests.Controllers.Validators;

public class EditarObraValidatorTests
{
    private readonly EditarObraValidator _validator = new EditarObraValidator();
    private readonly Fixture _fixture = new Fixture();
    
    [Theory]
    [InlineData(null)]
    [InlineData("nome")]
    public void EditarObraValidator_DadoUmNomeValido_NaoDeveRetornarErros(string nome)
    {
        var request = _fixture.Build<EditarObraRequest>()
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .With(x => x.Nome, nome)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(" ")]
    [InlineData("012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789")]
    public void EditarObraValidator_DadoUmNomeInvalido_DeveRetornarErros(string nome)
    {
        var request = _fixture.Build<EditarObraRequest>()
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .With(x => x.Nome, nome)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData("123123")]
    [InlineData("descricao")]
    [InlineData(null)]
    public void EditarObraValidator_DadoUmDescricaoValido_NaoDeveRetornarErros(string descricao)
    {
        var request = _fixture.Build<EditarObraRequest>()
            .With(x => x.Descricao, descricao)
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(" ")]
    public void EditarObraValidator_DadoUmDescricaoInvalido_DeveRetornarErros(string descricao)
    {
        var request = _fixture.Build<EditarObraRequest>()
            .With(x => x.Descricao, descricao)
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData("Endereco")]
    [InlineData("Rua do Saber")]    
    [InlineData(null)]
    public void EditarObraValidator_DadoUmEnderecoValido_NaoDeveRetornarErros(string endereco)
    {
        var request = _fixture.Build<EditarObraRequest>()
            .With(x => x.Endereco, endereco)
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(" ")]
    public void EditarObraValidator_DadoUmEnderecoInvalido_DeveRetornarErros(string endereco)
    {
        var request = _fixture.Build<EditarObraRequest>()
            .With(x => x.Endereco, endereco)
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData(10.8)]
    [InlineData(1)]
    [InlineData(null)]
    public void EditarObraValidator_DadoUmValorValido_NaoDeveRetornarErros(double? valor)
    {
        var request = _fixture.Build<EditarObraRequest>()
            .With(x => x.Valor, valor)
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void EditarObraValidator_DadoUmValorInvalido_DeveRetornarErros(double? valor)
    {
        var request = _fixture.Build<EditarObraRequest>()
            .With(x => x.Valor, valor)
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData(TipoObra.Ambas)]
    [InlineData(TipoObra.Comercial)]
    [InlineData(TipoObra.Residencial)]
    [InlineData(null)]
    public void EditarObraValidator_DadoUmTipoValido_NaoDeveRetornarErros(TipoObra? tipoObra)
    {
        var request = _fixture.Build<EditarObraRequest>()
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .With(x => x.TipoObra, tipoObra)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(1455767)]
    public void EditarObraValidator_DadoUmTipoInvalido_DeveRetornarErros(int? tipoObra)
    {
        var request = _fixture.Build<EditarObraRequest>()
            .With(x => x.TipoObra, tipoObra is null ? null : (TipoObra)tipoObra)
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }

    [Fact]
    public void EditarObraValidator_DadoUmPrazoConclusaoValido_NaoDeveRetornarErros()
    {
        var request = _fixture.Build<EditarObraRequest>()
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void EditarObraValidator_DadoUmPrazoConclusaoNullo_NaoDeveRetornarErros()
    {
        var request = _fixture.Build<EditarObraRequest>()
            .Without(x => x.PrazoConclusao)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("2022-10-13")]
    [InlineData("2022-09-13")]
    [InlineData("2022-01-13")]
    public void EditarObraValidator_DadoUmPrazoConclusaoInvalido_DeveRetornarErros(string prazoConclusaoString)
    {
        var prazoConclusao = (prazoConclusaoString is null) ? (DateTime?)null : DateTime.Parse(prazoConclusaoString);
        
        var request = _fixture.Build<EditarObraRequest>()
            .With(x => x.PrazoConclusao, prazoConclusao)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData(10)]
    [InlineData(1)]
    [InlineData(null)]
    public void EditarObraValidator_DadoUmOrcamentoIdValido_NaoDeveRetornarErros(int? orcamentoId)
    {
        var request = _fixture.Build<EditarObraRequest>()
            .With(x => x.OrcamentoId, orcamentoId)
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void EditarObraValidator_DadoUmOrcamentoIdInvalido_DeveRetornarErros(int? orcamentoId)
    {
        var request = _fixture.Build<EditarObraRequest>()
            .With(x => x.OrcamentoId, orcamentoId)
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
}