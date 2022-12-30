using AutoFixture;
using AutoMapper;
using ConstrutoraViverSA.Application.Services;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Exceptions;
using ConstrutoraViverSA.Repository.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace ConstrutoraViverSA.Application.IntegrationTests.Services;

public class FuncionarioServiceTests
{
    private readonly Mock<IFuncionarioRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Fixture _fixture = new Fixture();
    private readonly FuncionarioService _service; 
    private const string CPF_VALIDO = "14533067573";
    private const string CPF_INVALIDO = "14536067573";
    private const string EMAIL_VALIDO = "email@email";
    private const string EMAIL_INVALIDO = "email@";

    public FuncionarioServiceTests()
    {
        _repositoryMock = new Mock<IFuncionarioRepository>();
        _mapperMock = new Mock<IMapper>();
        _service = new FuncionarioService(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public void BuscarTodos_ComDadosValidos_DeveRealizarOperacao()
    {
        var funcionarios = _fixture.CreateMany<Funcionario>().ToList();
        var funcionariosDto = _fixture.CreateMany<FuncionarioDto>(3).ToList();
        _repositoryMock.Setup(x => x.BuscarTodos()).Returns(funcionarios);
        _mapperMock.SetupSequence(x => x.Map<FuncionarioDto>(It.IsAny<Funcionario>()))
            .Returns(funcionariosDto[0])
            .Returns(funcionariosDto[1])
            .Returns(funcionariosDto[2]);

        var resultado = _service.BuscarTodos();

        resultado.Count.Should().Be(funcionarios.Count);
        resultado.Should().BeEquivalentTo(funcionariosDto);
        _repositoryMock.Verify(x => x.BuscarTodos(), Times.Once);
        _mapperMock.Verify(x => x.Map<FuncionarioDto>(It.IsAny<Funcionario>()), Times.Exactly(3));
    }

    [Fact]
    public void BuscarEntidadePorId_ComDadosValidos_DeveRealizarOperacao()
    {
        var funcionarioId = 1;
        var funcionario = _fixture.Build<Funcionario>()
            .With(x => x.Id, funcionarioId)
            .Create();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == funcionarioId)))
            .Returns(funcionario);

        var resultado = _service.BuscarEntidadePorId(funcionarioId);

        resultado.Should().NotBeNull();
        resultado.Should().BeOfType<Funcionario>();
        resultado.Should().BeEquivalentTo(funcionario);
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == funcionarioId)), Times.Once);
    }
    
    [Fact]
    public void BuscarEntidadePorId_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var funcionarioId = 1;
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == funcionarioId)))
            .Throws<NaoEncontradoException>();

        var resultado = () => _service.BuscarEntidadePorId(funcionarioId);

        resultado.Should().Throw<NaoEncontradoException>();
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == funcionarioId)), Times.Once);
    }
    
    [Fact]
    public void BuscarPorId_ComDadosValidos_DeveRealizarOperacao()
    {
        var funcionarioId = 1;
        var funcionario = _fixture.Build<Funcionario>()
            .With(x => x.Id, funcionarioId)
            .Create();
        var funcionarioDto = _fixture.Create<FuncionarioDto>();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == funcionarioId)))
            .Returns(funcionario);
        _mapperMock.Setup(x => x.Map<FuncionarioDto>(It.Is<Funcionario>(x => x == funcionario)))
            .Returns(funcionarioDto);

        var resultado = _service.BuscarPorId(funcionarioId);

        resultado.Should().NotBeNull();
        resultado.Should().BeOfType<FuncionarioDto>();
        resultado.Should().BeEquivalentTo(funcionarioDto);
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == funcionarioId)), Times.Once);
        _mapperMock.Verify(x => x.Map<FuncionarioDto>(It.Is<Funcionario>(x => x == funcionario)), Times.Once);
    }
    
    [Fact]
    public void BuscarPorId_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var funcionarioId = 1;
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == funcionarioId)))
            .Throws<NaoEncontradoException>();

        var resultado = () => _service.BuscarPorId(funcionarioId);

        resultado.Should().Throw<NaoEncontradoException>();
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == funcionarioId)), Times.Once);
    }

    [Fact]
    public void Adicionar_ComDadosValidos_DeveRealizarOperacao()
    {
        var dto = _fixture.Build<FuncionarioDto>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .Create();
        _repositoryMock.Setup(x => x.Adicionar(It.IsAny<Funcionario>()));

        var resultado = () => _service.Adicionar(dto);

        resultado.Should().NotThrow<FuncionarioInvalidoException>();
        _repositoryMock.Verify(x => x.Adicionar(It.IsAny<Funcionario>()), Times.Once);
    }
    
    [Fact]
    public void Adicionar_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var dto = _fixture.Build<FuncionarioDto>()
            .With(x => x.Cpf, CPF_INVALIDO)
            .With(x => x.Email, EMAIL_INVALIDO)
            .Create();

        var resultado = () => _service.Adicionar(dto);

        resultado.Should().Throw<FuncionarioInvalidoException>();
    }
    
    [Fact]
    public void Editar_ComDadosValidos_DeveRealizarOperacao()
    {
        var funcionarioId = 1;
        var dto = _fixture.Build<FuncionarioDto>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .Create();
        var funcionario = _fixture.Build<Funcionario>()
            .With(x => x.Id, funcionarioId)
            .Create();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == funcionarioId))).Returns(funcionario);
        _repositoryMock.Setup(x => x.Editar(It.IsAny<Funcionario>()));

        var resultado = () => _service.Editar(funcionarioId, dto);

        resultado.Should().NotThrow<NaoEncontradoException>();
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == funcionarioId)), Times.Once);
        _repositoryMock.Verify(x => x.Editar(It.IsAny<Funcionario>()), Times.Once);
    }
    
    [Fact]
    public void Editar_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var funcionarioId = 1;
        var dto = _fixture.Build<FuncionarioDto>()
            .With(x => x.Cpf, CPF_INVALIDO)
            .With(x => x.Email, EMAIL_INVALIDO)
            .Create();
        var funcionario = _fixture.Build<Funcionario>()
            .With(x => x.Id, funcionarioId)
            .Create();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == funcionarioId))).Returns(funcionario);
        _repositoryMock.Setup(x => x.Editar(It.IsAny<Funcionario>()));

        var resultado = () => _service.Editar(funcionarioId, dto);
        
        resultado.Should().NotThrow<NaoEncontradoException>();
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == funcionarioId)), Times.Once);
        _repositoryMock.Verify(x => x.Editar(It.IsAny<Funcionario>()), Times.Once); 
    }
    
    [Fact]
    public void Excluir_ComDadosValidos_DeveRealizarOperacao()
    {
        var funcionarioId = 1;
        var funcionario = _fixture.Build<Funcionario>()
            .With(x => x.Id, funcionarioId)
            .Create();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == funcionarioId))).Returns(funcionario);
        _repositoryMock.Setup(x => x.Excluir(It.Is<Funcionario>(x => x == funcionario)));

        var resultado = () => _service.Excluir(funcionarioId);

        resultado.Should().NotThrow<NaoEncontradoException>();
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == funcionarioId)), Times.Once);
        _repositoryMock.Verify(x => x.Excluir(It.Is<Funcionario>(x => x == funcionario)), Times.Once);
    }
    
    [Fact]
    public void Excluir_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var funcionarioId = 1;
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == funcionarioId)))
            .Throws<NaoEncontradoException>();

        var resultado = () => _service.Excluir(funcionarioId);

        resultado.Should().Throw<NaoEncontradoException>();
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == funcionarioId)), Times.Once);
        _repositoryMock.Verify(x => x.Excluir(It.IsAny<Funcionario>()), Times.Never);
    }
}