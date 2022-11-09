using System;
using AutoFixture;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Api.Controllers.Validators;
using ConstrutoraViverSA.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace ConstrutoraViverSA.Api.Tests.Controllers.Validators;

public class CriarFuncionarioValidatorTests
{
    private readonly CriarFuncionarioValidator _validator = new CriarFuncionarioValidator();
    private readonly Fixture _fixture = new Fixture();
    private const string CPF_VALIDO = "14533067573";
    private const string CPF_INVALIDO = "14536067573";
    private const string EMAIL_VALIDO = "email@email";

    [Fact]
    public void CriarFuncionarioValidator_DadoUmNomeValido_NaoDeveRetornarErros()
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData(" ")]
    [InlineData("012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789")]
    public void CriarFuncionarioValidator_DadoUmNomeInvalido_DeveRetornarErros(string nome)
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .With(x => x.Nome, nome)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void CriarFuncionarioValidator_DadoUmaDataNascimentoValida_NaoDeveRetornarErros()
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .With(x => x.DataNascimento, DateTime.Today)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }

    [Fact]
    public void CriarFuncionarioValidator_DadoUmaDataNascimentoInvalida_DeveRetornarErros()
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .Without(x => x.DataNascimento)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData(Genero.Feminino)]
    [InlineData(Genero.Masculino)]
    [InlineData(Genero.NaoBinario)]
    [InlineData(Genero.Outro)]
    public void CriarFuncionarioValidator_DadoUmGeneroValido_NaoDeveRetornarErros(Genero genero)
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .With(x => x.Genero, genero)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(1455767)]
    [InlineData(null)]
    public void CriarFuncionarioValidator_DadoUmGeneroInvalido_DeveRetornarErros(int? genero)
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .With(x => x.Genero, genero is null ? null : (Genero)genero)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void CriarFuncionarioValidator_DadoUmCpfValido_NaoDeveRetornarErros()
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(" ")]
    [InlineData(null)]
    [InlineData("12345678999")]
    [InlineData("1234567899")]
    [InlineData(CPF_INVALIDO)]
    public void CriarFuncionarioValidator_DadoUmCpfInvalido_DeveRetornarErros(string cpf)
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, cpf)
            .With(x => x.Email, EMAIL_VALIDO)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData("123123")]
    [InlineData("numCtpsValido")]
    public void CriarFuncionarioValidator_DadoUmNumCtpsValido_NaoDeveRetornarErros(string numCtps)
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .With(x => x.NumCtps, numCtps)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(" ")]
    [InlineData(null)]
    public void CriarFuncionarioValidator_DadoUmNumCtpsInvalido_DeveRetornarErros(string numCtps)
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .With(x => x.NumCtps, numCtps)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData("Endereco")]
    [InlineData("Rua do Saber")]
    public void CriarFuncionarioValidator_DadoUmEnderecoValido_NaoDeveRetornarErros(string endereco)
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .With(x => x.Endereco, endereco)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(" ")]
    [InlineData(null)]
    public void CriarFuncionarioValidator_DadoUmEnderecoInvalido_DeveRetornarErros(string endereco)
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .With(x => x.Endereco, endereco)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData("Telefone")]
    [InlineData("49988706208")]
    public void CriarFuncionarioValidator_DadoUmTelefoneValido_NaoDeveRetornarErros(string telefone)
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .With(x => x.Telefone, telefone)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(" ")]
    [InlineData(null)]
    public void CriarFuncionarioValidator_DadoUmTelefoneInvalido_DeveRetornarErros(string telefone)
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .With(x => x.Telefone, telefone)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData("Email@email.com")]
    [InlineData("test@test")]
    [InlineData("test@test.com.br")]
    [InlineData(EMAIL_VALIDO)]
    public void CriarFuncionarioValidator_DadoUmEmailValido_NaoDeveRetornarErros(string email)
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, email)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(" ")]
    [InlineData("teste")]
    [InlineData("@teste.com")]
    [InlineData(null)]
    public void CriarFuncionarioValidator_DadoUmEmailInvalido_DeveRetornarErros(string email)
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, email)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData(Cargo.Almoxarife)]
    [InlineData(Cargo.Arquiteto)]
    [InlineData(Cargo.AnalistaFinanceiro)]
    [InlineData(Cargo.MestreObras)]
    public void CriarFuncionarioValidator_DadoUmCargoValido_NaoDeveRetornarErros(Cargo cargo)
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .With(x => x.Cargo, cargo)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData(2143643)]
    public void CriarFuncionarioValidator_DadoUmCargoInvalido_DeveRetornarErros(int? cargo)
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .With(x => x.Cargo, cargo is null ? null : (Cargo)cargo)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
}