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

public class CriarObraValidatorTests
{
    private readonly CriarObraValidator _validator = new CriarObraValidator();
    private readonly Fixture _fixture = new Fixture();
    private static readonly List<long> LISTA_FUNCIONARIOS_INVALIDA = new ();
    private static readonly List<long> LISTA_FUNCIONARIOS_VALIDA = new () { 1,2,3 };
    private static readonly Dictionary<long, int> DICIONARIO_MATERIAIS_INVALIDO = new();
    private static readonly Dictionary<long, int> DICIONARIO_MATERIAIS_VALIDO = new() { {1, 2}, {2,3}};
    
    [Fact]
    public void CriarObraValidator_DadoUmNomeValido_NaoDeveRetornarErros()
    {
        var request = _fixture.Build<ObraRequest>()
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData(" ")]
    [InlineData("012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789")]
    public void CriarObraValidator_DadoUmNomeInvalido_DeveRetornarErros(string nome)
    {
        var request = _fixture.Build<ObraRequest>()
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .With(x => x.Nome, nome)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData("123123")]
    [InlineData("descricao")]
    public void CriarObraValidator_DadoUmDescricaoValido_NaoDeveRetornarErros(string descricao)
    {
        var request = _fixture.Build<ObraRequest>()
            .With(x => x.Descricao, descricao)
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(" ")]
    [InlineData(null)]
    public void CriarObraValidator_DadoUmDescricaoInvalido_DeveRetornarErros(string descricao)
    {
        var request = _fixture.Build<ObraRequest>()
            .With(x => x.Descricao, descricao)
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData("Endereco")]
    [InlineData("Rua do Saber")]
    public void CriarObraValidator_DadoUmEnderecoValido_NaoDeveRetornarErros(string endereco)
    {
        var request = _fixture.Build<ObraRequest>()
            .With(x => x.Endereco, endereco)
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(" ")]
    [InlineData(null)]
    public void CriarObraValidator_DadoUmEnderecoInvalido_DeveRetornarErros(string endereco)
    {
        var request = _fixture.Build<ObraRequest>()
            .With(x => x.Endereco, endereco)
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData(10.8)]
    [InlineData(1)]
    public void CriarObraValidator_DadoUmValorValido_NaoDeveRetornarErros(double valor)
    {
        var request = _fixture.Build<ObraRequest>()
            .With(x => x.Valor, valor)
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(null)]
    public void CriarObraValidator_DadoUmValorInvalido_DeveRetornarErros(double? valor)
    {
        var request = _fixture.Build<ObraRequest>()
            .With(x => x.Valor, valor)
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData(TipoObraEnum.Ambas)]
    [InlineData(TipoObraEnum.Comercial)]
    [InlineData(TipoObraEnum.Residencial)]
    public void CriarObraValidator_DadoUmTipoValido_NaoDeveRetornarErros(TipoObraEnum tipoObra)
    {
        var request = _fixture.Build<ObraRequest>()
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .With(x => x.TipoObra, tipoObra)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(1455767)]
    [InlineData(null)]
    public void CriarObraValidator_DadoUmTipoInvalido_DeveRetornarErros(int? tipoObra)
    {
        var request = _fixture.Build<ObraRequest>()
            .With(x => x.TipoObra, tipoObra is null ? null : (TipoObraEnum)tipoObra)
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }

    [Fact]
    public void CriarObraValidator_DadoUmPrazoConclusaoValido_NaoDeveRetornarErros()
    {
        var request = _fixture.Build<ObraRequest>()
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    
    [Theory]
    [InlineData(null)]
    [InlineData("2022-10-13")]
    public void CriarObraValidator_DadoUmPrazoConclusaoInvalido_DeveRetornarErros(string prazoConclusaoString)
    {
        var prazoConclusao = (prazoConclusaoString is null) ? (DateTime?)null : DateTime.Parse(prazoConclusaoString);
        
        var request = _fixture.Build<ObraRequest>()
            .With(x => x.PrazoConclusao, prazoConclusao)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData(10)]
    [InlineData(1)]
    public void CriarObraValidator_DadoUmOrcamentoIdValido_NaoDeveRetornarErros(int orcamentoId)
    {
        var request = _fixture.Build<ObraRequest>()
            .With(x => x.OrcamentoId, orcamentoId)
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(null)]
    public void CriarObraValidator_DadoUmOrcamentoIdInvalido_DeveRetornarErros(int? orcamentoId)
    {
        var request = _fixture.Build<ObraRequest>()
            .With(x => x.OrcamentoId, orcamentoId)
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }

    [Fact]
    public void CriarObraValidator_DadoUmaListaDeFuncionariosValida_NaoDeveRetornarErros()
    {
        var request = _fixture.Build<ObraRequest>()
            .With(x => x.Funcionarios, LISTA_FUNCIONARIOS_VALIDA)
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void CriarObraValidator_DadoUmaListaDeFuncionariosNulla_NaoDeveRetornarErros()
    {
        var request = _fixture.Build<ObraRequest>()
            .Without(x => x.Funcionarios)
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void CriarObraValidator_DadoUmaListaDeFuncionariosInvalida_DeveRetornarErros()
    {
        var request = _fixture.Build<ObraRequest>()
            .With(x => x.Funcionarios, LISTA_FUNCIONARIOS_INVALIDA)
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void CriarObraValidator_DadoUmDicionarioDeMateriaisValido_NaoDeveRetornarErros()
    {
        var request = _fixture.Build<ObraRequest>()
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .With(x => x.Materiais, DICIONARIO_MATERIAIS_VALIDO)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void CriarObraValidator_DadoUmDicionarioDeMateriaisNullo_NaoDeveRetornarErros()
    {
        var request = _fixture.Build<ObraRequest>()
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Without(x => x.Materiais)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void CriarObraValidator_DadoUmDicionarioDeMateriaisInvalido_DeveRetornarErros()
    {
        var request = _fixture.Build<ObraRequest>()
            .With(x => x.Materiais, DICIONARIO_MATERIAIS_INVALIDO)
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
}