using System;
using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Api.Controllers.Validators;
using ConstrutoraViverSA.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace ConstrutoraViverSA.Api.Tests.Controllers.Validators;

public class EditarFuncionarioValidatorTests
{
    private readonly EditarFuncionarioValidator _validator = new EditarFuncionarioValidator();
    private readonly Fixture _fixture = new Fixture();
    private const string CPF_VALIDO = "14533067573";
    private const string CPF_INVALIDO = "14536067573";
    private const string EMAIL_VALIDO = "email@email";

    [Theory]
    [InlineData("nome")]
    [InlineData(null)]
    public void EditarFuncionarioValidator_DadoUmNomeValido_NaoDeveRetornarErros(string nome)
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .With(x => x.Nome, nome)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(" ")]
    [InlineData("012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789")]
    public void EditarFuncionarioValidator_DadoUmNomeInvalido_DeveRetornarErros(string nome)
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .With(x => x.Nome, nome)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData("2000-10-10")]
    [InlineData(null)]
    public void EditarFuncionarioValidator_DadoUmaDataNascimentoValida_NaoDeveRetornarErros(string dataNascimentoString)
    {
        var dataNascimento = (dataNascimentoString is not null) ? DateTime.Parse(dataNascimentoString) : (DateTime?)null;
        
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .With(x => x.DataNascimento, dataNascimento)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(GeneroEnum.Feminino)]
    [InlineData(GeneroEnum.Masculino)]
    [InlineData(GeneroEnum.NaoBinario)]
    [InlineData(GeneroEnum.Outro)]
    [InlineData(null)]
    public void EditarFuncionarioValidator_DadoUmGeneroValido_NaoDeveRetornarErros(GeneroEnum? genero)
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
    [InlineData(1389874)]
    public void EditarFuncionarioValidator_DadoUmGeneroInvalido_DeveRetornarErros(int genero)
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .With(x => x.Genero, (GeneroEnum)genero)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData(CPF_VALIDO)]
    [InlineData(null)]
    public void EditarFuncionarioValidator_DadoUmCpfValido_NaoDeveRetornarErros(string cpf)
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, cpf)
            .With(x => x.Email, EMAIL_VALIDO)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(" ")]
    [InlineData("12345678999")]
    [InlineData("1234567899")]
    [InlineData(CPF_INVALIDO)]
    public void EditarFuncionarioValidator_DadoUmCpfInvalido_DeveRetornarErros(string cpf)
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
    [InlineData(null)]
    public void EditarFuncionarioValidator_DadoUmNumCtpsValido_NaoDeveRetornarErros(string numCtps)
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
    public void EditarFuncionarioValidator_DadoUmNumCtpsInvalido_DeveRetornarErros(string numCtps)
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
    [InlineData(null)]
    public void EditarFuncionarioValidator_DadoUmEnderecoValido_NaoDeveRetornarErros(string endereco)
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
    public void EditarFuncionarioValidator_DadoUmEnderecoInvalido_DeveRetornarErros(string endereco)
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
    [InlineData(null)]
    public void EditarFuncionarioValidator_DadoUmTelefoneValido_NaoDeveRetornarErros(string telefone)
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
    public void EditarFuncionarioValidator_DadoUmTelefoneInvalido_DeveRetornarErros(string telefone)
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
    [InlineData(null)]
    public void EditarFuncionarioValidator_DadoUmEmailValido_NaoDeveRetornarErros(string email)
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
    public void EditarFuncionarioValidator_DadoUmEmailInvalido_DeveRetornarErros(string email)
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, email)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData(CargoEnum.Almoxarife)]
    [InlineData(CargoEnum.Arquiteto)]
    [InlineData(CargoEnum.AnalistaFinanceiro)]
    [InlineData(CargoEnum.MestreObras)]
    [InlineData(null)]
    public void EditarFuncionarioValidator_DadoUmCargoValido_NaoDeveRetornarErros(CargoEnum? cargo)
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
    [InlineData(243)]
    public void EditarFuncionarioValidator_DadoUmCargoInvalido_DeveRetornarErros(int cargo)
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .With(x => x.Cargo, (CargoEnum)cargo)
            .Create();
        
        var validationResult = _validator.Validate(request);
        
        validationResult.IsValid.Should().BeFalse();
    }
}