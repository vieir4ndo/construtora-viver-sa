using System;
using AutoFixture;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace ConstrutoraViverSA.Api.Tests.Controllers.Requests;

public class FuncionarioRequestTests
{
    private readonly Fixture _fixture = new Fixture();
    private const string CPF_VALIDO = "14533067573";
    private const string CPF_INVALIDO = "14536067573";

    [Fact]
    public void ValidarCriacao_ComDadosValidos_DeveSeComportarComoEsperado()
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.DataNascimento, DateTime.Today)
            .With(x => x.Email, "email@email.com")
            .Create();

        var result = () => request.ValidarCriacao();

        result.Should().NotThrow<ErroValidacaoException>();
    }
    
    [Theory]
    [InlineData(null, "2000-06-20", GeneroEnum.Feminino, CPF_VALIDO, "numCtps", "endereco", "email@email.com", "49988703656", CargoEnum.Almoxarife)]
    [InlineData(" ", "2000-06-20", GeneroEnum.Feminino, CPF_VALIDO, "numCtps", "endereco", "email@email.com", "49988703656", CargoEnum.Almoxarife)]
    [InlineData("012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789", "2000-06-20", GeneroEnum.Feminino, CPF_VALIDO, "numCtps", "endereco", "email@email.com", "49988703656", CargoEnum.Almoxarife)]
    [InlineData("nome", null, GeneroEnum.Feminino, CPF_VALIDO, "numCtps", "endereco", "email@email.com", "49988703656", CargoEnum.Almoxarife)]
    [InlineData("nome", "2000-06-20", null, CPF_VALIDO, "numCtps", "endereco", "email@email.com", "49988703656", CargoEnum.Almoxarife)]
    [InlineData("nome", "2000-06-20", GeneroEnum.Feminino, null, "numCtps", "endereco", "email@email.com", "49988703656", CargoEnum.Almoxarife)]
    [InlineData("nome", "2000-06-20", GeneroEnum.Feminino, " ", "numCtps", "endereco", "email@email.com", "49988703656", CargoEnum.Almoxarife)]
    [InlineData("nome", "2000-06-20", GeneroEnum.Feminino, CPF_INVALIDO, "numCtps", "endereco", "email@email.com", "49988703656", CargoEnum.Almoxarife)]
    [InlineData("nome", "2000-06-20", GeneroEnum.Feminino, CPF_VALIDO, null, "endereco", "email@email.com", "49988703656", CargoEnum.Almoxarife)]
    [InlineData("nome", "2000-06-20", GeneroEnum.Feminino, CPF_VALIDO, " ", "endereco", "email@email.com", "49988703656", CargoEnum.Almoxarife)]
    [InlineData("nome", "2000-06-20", GeneroEnum.Feminino, CPF_VALIDO, "numCtps", null, "email@email.com", "49988703656", CargoEnum.Almoxarife)]
    [InlineData("nome", "2000-06-20", GeneroEnum.Feminino, CPF_VALIDO, "numCtps", " ", "email@email.com", "49988703656", CargoEnum.Almoxarife)]
    [InlineData("nome", "2000-06-20", GeneroEnum.Feminino, CPF_VALIDO, "numCtps", "endereco", " ", "49988703656", CargoEnum.Almoxarife)]
    [InlineData("nome", "2000-06-20", GeneroEnum.Feminino, CPF_VALIDO, "numCtps", "endereco", null, "49988703656", CargoEnum.Almoxarife)]
    [InlineData("nome", "2000-06-20", GeneroEnum.Feminino, CPF_VALIDO, "numCtps", "endereco", "email@email.com", " ", CargoEnum.Almoxarife)]
    [InlineData("nome", "2000-06-20", GeneroEnum.Feminino, CPF_VALIDO, "numCtps", "endereco", "email@email.com", null, CargoEnum.Almoxarife)]
    [InlineData("nome", "2000-06-20", GeneroEnum.Feminino, CPF_VALIDO, "numCtps", "endereco", "email@email.com", "49988703656", null)]
    public void ValidarCriacao_ComDadosInvalidos_DeveSeComportarComoEsperado(string nome, string dataNascimento, GeneroEnum? genero, string cpf, string numCtps, string endereco, string email, string telefone, CargoEnum? cargo)
    {
        var request = new FuncionarioRequest()
        {
            Nome = nome,
            DataNascimento = dataNascimento is not null ? DateTime.Parse(dataNascimento) : null,
            Genero = genero,
            Cpf = cpf,
            NumCtps = numCtps,
            Endereco = endereco,
            Email = email,
            Telefone = telefone,
            Cargo = cargo
        };
        
        var result = () => request.ValidarCriacao();
        
        result.Should().Throw<ErroValidacaoException>();
    }
    
    [Fact]
    public void ValidarEdicao_ComDadosValidos_DeveSeComportarComoEsperado()
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, "email@email.com")
            .With(x => x.DataNascimento, DateTime.Today)
            .Create();
        
        var result = () => request.ValidarEdicao();

        result.Should().NotThrow<ErroValidacaoException>();
    }
    
    [Theory]
    [InlineData(" ", "2000-06-20", GeneroEnum.Feminino, CPF_VALIDO, "numCtps", "endereco", "email@email.com", "49988703656", CargoEnum.Almoxarife)]
    [InlineData("012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789", "2000-06-20", GeneroEnum.Feminino, CPF_VALIDO, "numCtps", "endereco", "email@email.com", "49988703656", CargoEnum.Almoxarife)]
    [InlineData("nome", "2000-06-20", GeneroEnum.Feminino, " ", "numCtps", "endereco", "email@email.com", "49988703656", CargoEnum.Almoxarife)]
    [InlineData("nome", "2000-06-20", GeneroEnum.Feminino, CPF_INVALIDO, "numCtps", "endereco", "email@email.com", "49988703656", CargoEnum.Almoxarife)]
    [InlineData("nome", "2000-06-20", GeneroEnum.Feminino, CPF_VALIDO, " ", "endereco", "email@email.com", "49988703656", CargoEnum.Almoxarife)]
    [InlineData("nome", "2000-06-20", GeneroEnum.Feminino, CPF_VALIDO, "numCtps", " ", "email@email.com", "49988703656", CargoEnum.Almoxarife)]
    [InlineData("nome", "2000-06-20", GeneroEnum.Feminino, CPF_VALIDO, "numCtps", "endereco", " ", "49988703656", CargoEnum.Almoxarife)]
    [InlineData("nome", "2000-06-20", GeneroEnum.Feminino, CPF_VALIDO, "numCtps", "endereco", "email@email.com", " ", CargoEnum.Almoxarife)]
    public void ValidarEdicao_ComDadosInvalidos_DeveSeComportarComoEsperado(string nome, string dataNascimento, GeneroEnum? genero, string cpf, string numCtps, string endereco, string email, string telefone, CargoEnum? cargo)
    {
        var request = new FuncionarioRequest()
        {
            Nome = nome,
            DataNascimento = dataNascimento is not null ? DateTime.Parse(dataNascimento) : null,
            Genero = genero,
            Cpf = cpf,
            NumCtps = numCtps,
            Endereco = endereco,
            Email = email,
            Telefone = telefone,
            Cargo = cargo
        };
        
        var result = () => request.ValidarCriacao();
        
        result.Should().Throw<ErroValidacaoException>();
    }
}