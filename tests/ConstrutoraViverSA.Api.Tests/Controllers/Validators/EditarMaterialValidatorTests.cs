using AutoFixture;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Api.Controllers.Validators;
using ConstrutoraViverSA.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace ConstrutoraViverSA.Api.Tests.Controllers.Validators;

public class EditarMaterialValidatorTests
{
    private readonly EditarMaterialValidator _validator = new EditarMaterialValidator();
    private readonly Fixture _fixture = new Fixture();

    [Theory]
    [InlineData("descricao")]
    [InlineData(null)]
    public void EditarMaterialValidator_DadoUmNomeValido_NaoDeveRetornarErros(string descricao)
    {
        var request = _fixture.Build<EditarMaterialRequest>()
            .With(x => x.Descricao, descricao)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(" ")]
    [InlineData("012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789")]
    public void EditarMaterialValidator_DadoUmNomeInvalido_DeveRetornarErros(string nome)
    {
        var request = _fixture.Build<EditarMaterialRequest>()
            .With(x => x.Nome, nome)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData("123123")]
    [InlineData("descricao")]
    [InlineData(null)]
    public void EditarMaterialValidator_DadoUmDescricaoValido_NaoDeveRetornarErros(string descricao)
    {
        var request = _fixture.Build<EditarMaterialRequest>()
            .With(x => x.Descricao, descricao)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(" ")]
    public void EditarMaterialValidator_DadoUmDescricaoInvalido_DeveRetornarErros(string descricao)
    {
        var request = _fixture.Build<EditarMaterialRequest>()
            .With(x => x.Descricao, descricao)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData(TipoMaterial.Cimento)]
    [InlineData(TipoMaterial.Madeira)]
    [InlineData(TipoMaterial.Telha)]
    [InlineData(TipoMaterial.Tijolo)]    
    [InlineData(null)]
    public void CriarFuncionarioValidator_DadoUmTipoValido_NaoDeveRetornarErros(TipoMaterial? tipoMaterial)
    {
        var request = _fixture.Build<EditarMaterialRequest>()
            .With(x => x.Tipo, tipoMaterial)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(100)]
    public void CriarFuncionarioValidator_DadoUmTipoInvalido_DeveRetornarErros(int tipoMaterial)
    {
        var request = _fixture.Build<EditarMaterialRequest>()
            .With(x => x.Tipo, (TipoMaterial)tipoMaterial)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData(10.8)]
    [InlineData(1)]
    [InlineData(null)]
    public void EditarMaterialValidator_DadoUmValorValido_NaoDeveRetornarErros(double? valor)
    {
        var request = _fixture.Build<EditarMaterialRequest>()
            .With(x => x.Valor, valor)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(-1)]
    public void EditarMaterialValidator_DadoUmValorInvalido_DeveRetornarErros(double? valor)
    {
        var request = _fixture.Build<EditarMaterialRequest>()
            .With(x => x.Valor, valor)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
}