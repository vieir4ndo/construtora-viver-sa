using System.Linq;
using AutoFixture;
using AutoMapper;
using ConstrutoraViverSA.Api.Controllers;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Application.Interfaces;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace ConstrutoraViverSA.Api.UnitTests.Controllers;

public class FuncionarioControllerTests
{
    private readonly Mock<IFuncionarioService> _funcionarioServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly FuncionarioController _controller;
    private readonly Fixture _fixture = new Fixture();
    private const string CPF_VALIDO = "14533067573";
    private const string CPF_INVALIDO = "14536067573";
    private const string EMAIL_VALIDO = "email@email";
    private const string EMAIL_INVALIDO = "email@";

    public FuncionarioControllerTests()
    {
        _funcionarioServiceMock = new Mock<IFuncionarioService>();
        _mapperMock = new Mock<IMapper>();
        _controller = new FuncionarioController(_funcionarioServiceMock.Object, _mapperMock.Object);
    }

    [Fact]
    public void CadastrarFuncionario_ComDadosValidos_DeveRealizarOperacao()
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .Create();
        var dto = _fixture.Build<FuncionarioDto>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .Create();
        _mapperMock.Setup(x => x.Map<FuncionarioDto>(It.Is<FuncionarioRequest>(x => x == request))).Returns(dto);
        _funcionarioServiceMock.Setup(x => x.Adicionar(It.Is<FuncionarioDto>(x => x == dto)));

        var resultado = _controller.CadastrarFuncionario(request);

        resultado.Sucesso.Should().BeTrue();
        resultado.Dados.Should().BeNull();
        resultado.Mensagens.Should().BeNull();
        _funcionarioServiceMock.Verify(X => X.Adicionar(It.Is<FuncionarioDto>(x => x == dto)), Times.Once);
        _mapperMock.Verify(x => x.Map<FuncionarioDto>(It.Is<FuncionarioRequest>(x => x == request)), Times.Once);
    }
    
    [Fact]
    public void CadastrarFuncionario_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_INVALIDO)
            .With(x => x.Email, EMAIL_INVALIDO)
            .Create();
     
        var resultado = () => _controller.CadastrarFuncionario(request);
        
        resultado.Should().Throw<ErroValidacaoException>();
       }

    [Fact]
    public void BuscarFuncionario_ComDadosValidos_DeveRealizarOperacao()
    {
        var funcionarioId = 1;
        var funcionario = _fixture.Create<FuncionarioDto>();
        _funcionarioServiceMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == funcionarioId))).Returns(funcionario);

        var resultado = _controller.BuscarFuncionario(funcionarioId);

        resultado.Sucesso.Should().BeTrue();
        resultado.Mensagens.Should().BeNull();
        resultado.Dados.Should().NotBeNull();
        resultado.Dados.First().Should().BeOfType<FuncionarioDto>();
        resultado.Dados.First().Should().BeEquivalentTo(funcionario);
        _funcionarioServiceMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == funcionarioId)), Times.Once);
    }
    
    [Fact]
    public void BuscarFuncionario_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var funcionarioId = 1;
        _funcionarioServiceMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == funcionarioId)))
            .Throws(new NaoEncontradoException("Funcionario não encontrado"));

        var resultado = () => _controller.BuscarFuncionario(funcionarioId);

        resultado.Should().Throw<NaoEncontradoException>();
        _funcionarioServiceMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == funcionarioId)), Times.Once);
    }

    [Fact]
    public void BuscarFuncionarios_ComDadosValidos_DeveRealizarOperacao()
    {
        var listaFuncionarios = _fixture.CreateMany<FuncionarioDto>().ToList();
        _funcionarioServiceMock.Setup(x => x.BuscarTodos())
            .Returns(listaFuncionarios);

        var resultado = _controller.BuscarFuncionarios();

        resultado.Sucesso.Should().BeTrue();
        resultado.Mensagens.Should().BeNull();
        resultado.Dados.Should().NotBeNull();
        resultado.Dados.Count.Should().Be(listaFuncionarios.Count);
        resultado.Dados.ForEach(x => x.Should().BeOfType<FuncionarioDto>());
        for (var i =0; i< listaFuncionarios.Count; i++)
        {
            resultado.Dados[i].Should().BeEquivalentTo(listaFuncionarios[i]);
        }
        _funcionarioServiceMock.Verify(x => x.BuscarTodos(), Times.Once);
    }

    [Fact]
    public void EditarFuncionario_ComDadosValidos_DeveRealizarOperacao()
    {
        var funcionarioId = 1;
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .Create();
        var dto = _fixture.Build<FuncionarioDto>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .Create();
        _mapperMock.Setup(x => x.Map<FuncionarioDto>(It.Is<FuncionarioRequest>(x => x == request))).Returns(dto);
        _funcionarioServiceMock.Setup(x => x.Editar(It.Is<long>(x => x == funcionarioId),It.Is<FuncionarioDto>(x => x == dto)));

        var resultado = _controller.EditarFuncionario(request, funcionarioId);

        resultado.Sucesso.Should().BeTrue();
        resultado.Dados.Should().BeNull();
        resultado.Mensagens.Should().BeNull();
        _funcionarioServiceMock.Verify(X => X.Editar(It.Is<long>(x => x == funcionarioId),It.Is<FuncionarioDto>(x => x == dto)), Times.Once);
        _mapperMock.Verify(x => x.Map<FuncionarioDto>(It.Is<FuncionarioRequest>(x => x == request)), Times.Once);
    }

    [Fact]
    public void EditarFuncionario_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var funcionarioId = 1;
        var request = _fixture.Build<FuncionarioRequest>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .Create();
        _funcionarioServiceMock.Setup(x => x.Editar(It.Is<long>(x => x == funcionarioId), It.IsAny<FuncionarioDto>()))
            .Throws(new NaoEncontradoException("Funcionario não encontrado"));
        
        var resultado = () => _controller.EditarFuncionario(request, funcionarioId);
        
        resultado.Should().Throw<NaoEncontradoException>();
        _funcionarioServiceMock.Verify(x => x.Editar(It.Is<long>(x => x == funcionarioId), It.IsAny<FuncionarioDto>()), Times.Once);
    }

    [Fact]
    public void ExcluirFuncionario_ComDadosValidos_DeveRealizarOperacao()
    {
        var funcionarioId = 1;
        _funcionarioServiceMock.Setup(x => x.Excluir(It.Is<long>(x => x == funcionarioId)));

        var resultado = _controller.ExcluirFuncionario(funcionarioId);
        
        resultado.Sucesso.Should().BeTrue();
        resultado.Dados.Should().BeNull();
        resultado.Mensagens.Should().BeNull();
        _funcionarioServiceMock.Verify(x => x.Excluir(It.Is<long>(x => x == funcionarioId)), Times.Once);
    }
    
    [Fact]
    public void ExcluirFuncionario_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var funcionarioId = 1;
        _funcionarioServiceMock.Setup(x => x.Excluir(It.Is<long>(x => x == funcionarioId)))
            .Throws(new NaoEncontradoException("Funcionario não encontrado"));

        var resultado = () => _controller.ExcluirFuncionario(funcionarioId);
        
        resultado.Should().Throw<NaoEncontradoException>();
        _funcionarioServiceMock.Verify(x => x.Excluir(It.Is<long>(x => x == funcionarioId)), Times.Once);
    }
}