using AutoFixture;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Api.Controllers.Validators;
using ConstrutoraViverSA.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace ConstrutoraViverSA.Api.Tests.Controllers.Validators;

public class CriarMaterialValidatorTests
{
    private readonly CriarMaterialValidator _validator = new CriarMaterialValidator();
    private readonly Fixture _fixture = new Fixture();

    [Fact]
    public void CriarMaterialValidator_DadoUmNomeValido_NaoDeveRetornarErros()
    {
        var request = _fixture.Build<MaterialRequest>()
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData(" ")]
    [InlineData("012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789")]
    public void CriarMaterialValidator_DadoUmNomeInvalido_DeveRetornarErros(string nome)
    {
        var request = _fixture.Build<MaterialRequest>()
            .With(x => x.Nome, nome)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData("123123")]
    [InlineData("descricao")]
    public void CriarMaterialValidator_DadoUmDescricaoValido_NaoDeveRetornarErros(string descricao)
    {
        var request = _fixture.Build<MaterialRequest>()
            .With(x => x.Descricao, descricao)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(" ")]
    [InlineData(null)]
    public void CriarMaterialValidator_DadoUmDescricaoInvalido_DeveRetornarErros(string descricao)
    {
        var request = _fixture.Build<MaterialRequest>()
            .With(x => x.Descricao, descricao)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData(TipoMaterialEnum.Cimento)]
    [InlineData(TipoMaterialEnum.Madeira)]
    [InlineData(TipoMaterialEnum.Telha)]
    [InlineData(TipoMaterialEnum.Tijolo)]
    public void CriarFuncionarioValidator_DadoUmTipoValido_NaoDeveRetornarErros(TipoMaterialEnum tipoMaterial)
    {
        var request = _fixture.Build<MaterialRequest>()
            .With(x => x.Tipo, tipoMaterial)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(100)]
    [InlineData(null)]
    public void CriarFuncionarioValidator_DadoUmTipoInvalido_DeveRetornarErros(int? tipoMaterial)
    {
        var request = _fixture.Build<MaterialRequest>()
            .With(x => x.Tipo, tipoMaterial is null ? null : (TipoMaterialEnum)tipoMaterial)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData(10.8)]
    [InlineData(1)]
    public void CriarMaterialValidator_DadoUmValorValido_NaoDeveRetornarErros(double valor)
    {
        var request = _fixture.Build<MaterialRequest>()
            .With(x => x.Valor, valor)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(-1)]
    [InlineData(null)]
    public void CriarMaterialValidator_DadoUmValorInvalido_DeveRetornarErros(double? valor)
    {
        var request = _fixture.Build<MaterialRequest>()
            .With(x => x.Valor, valor)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData(10)]
    [InlineData(1)]
    public void CriarMaterialValidator_DadoUmQuantidadeValido_NaoDeveRetornarErros(int quantidade)
    {
        var request = _fixture.Build<MaterialRequest>()
            .With(x => x.Quantidade, quantidade)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(null)]
    public void CriarMaterialValidator_DadoUmQuantidadeInvalido_DeveRetornarErros(int? quantidade)
    {
        var request = _fixture.Build<MaterialRequest>()
            .With(x => x.Quantidade, quantidade)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
}