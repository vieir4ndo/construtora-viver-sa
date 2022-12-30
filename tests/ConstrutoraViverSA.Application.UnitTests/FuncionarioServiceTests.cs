using System.Linq;
using AutoFixture;
using AutoMapper;
using ConstrutoraViverSA.Application.Services;
using ConstrutoraViverSA.Domain.Dtos;
using FluentAssertions;
using Moq;
using Xunit;

namespace ConstrutoraViverSA.Application.UnitTests;

public class FuncionarioServiceTests
{
    private readonly FuncionarioRepositoryFake _repositoryFake;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Fixture _fixture = new Fixture();
    private readonly FuncionarioService _service;
    private const string CPF_VALIDO = "14533067573";
    private const string CPF_INVALIDO = "14536067573";
    private const string EMAIL_VALIDO = "email@email";
    private const string EMAIL_INVALIDO = "email@";
    
    public FuncionarioServiceTests()
    {
        _repositoryFake = new FuncionarioRepositoryFake();
        _mapperMock = new Mock<IMapper>();
        _service = new FuncionarioService(_repositoryFake, _mapperMock.Object);
    }
    
    [Fact]
    public void Adicionar_ComDadosValidos_DeveRealizarOperacao()
    {
        var dto = _fixture.Build<FuncionarioDto>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .Create();

        _service.Adicionar(dto);
        _service.Adicionar(dto);

        var funcionario = _repositoryFake.Funcionarios.First();

        funcionario.Nome.Should().Be(dto.Nome);
        funcionario.DataNascimento.Should().Be(dto.DataNascimento);
        funcionario.Genero.Should().Be(dto.Genero);
        funcionario.Cpf.Should().Be(dto.Cpf);
        funcionario.NumCtps.Should().Be(dto.NumCtps);
        funcionario.Endereco.Should().Be(dto.Endereco);
        funcionario.Email.Should().Be(dto.Email);
        funcionario.Telefone.Should().Be(dto.Telefone);
        funcionario.Cargo.Should().Be(dto.Cargo);
    }
}