using System;
using System.Collections.Generic;
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

namespace ConstrutoraViverSA.Api.Tests.Controllers;

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

        var result = () => _controller.CadastrarFuncionario(request);

        result.Should().NotThrow<ErroValidacaoException>();
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
        var dto = _fixture.Build<FuncionarioDto>()
            .With(x => x.Cpf, CPF_VALIDO)
            .With(x => x.Email, EMAIL_VALIDO)
            .Create();
        _mapperMock.Setup(x => x.Map<FuncionarioDto>(It.Is<FuncionarioRequest>(x => x == request))).Returns(dto);
        _funcionarioServiceMock.Setup(x => x.Adicionar(It.Is<FuncionarioDto>(x => x == dto)));

        Action result = () => _controller.CadastrarFuncionario(request);
        
        result.Should().Throw<ErroValidacaoException>();
        _funcionarioServiceMock.Verify(X => X.Adicionar(It.Is<FuncionarioDto>(x => x == dto)), Times.Never);
        _mapperMock.Verify(x => x.Map<FuncionarioDto>(It.Is<FuncionarioRequest>(x => x == request)), Times.Never);
    }

    [Fact]
    public void BuscarFuncionario_ComDadosValidos_DeveRealizarOperacao()
    {
        var funcionarioId = 1;
        var funcionario = _fixture.Create<FuncionarioDto>();
        _funcionarioServiceMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == funcionarioId))).Returns(funcionario);

        var result = _controller.BuscarFuncionario(funcionarioId);

        result.Sucesso.Should().BeTrue();
        result.Mensagens.Should().BeNull();
        result.Dados.Should().NotBeNull();
        result.Dados.First().Should().BeOfType<FuncionarioDto>();
        result.Dados.First().Should().BeEquivalentTo(funcionario);
        _funcionarioServiceMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == funcionarioId)), Times.Once);
    }
    
    [Fact]
    public void BuscarFuncionario_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var funcionarioId = 1;
        _funcionarioServiceMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == funcionarioId)))
            .Throws(new NaoEncontradoException("Funcionario não encontrado"));

        Action result = () => _controller.BuscarFuncionario(funcionarioId);

        result.Should().Throw<NaoEncontradoException>();
        _funcionarioServiceMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == funcionarioId)), Times.Once);
    }

    [Fact]
    public void BuscarFuncionarios_ComDadosValidos_DeveRealizarOperacao()
    {
        var listaFuncionarios = _fixture.CreateMany<FuncionarioDto>().ToList();
        _funcionarioServiceMock.Setup(x => x.BuscarTodos())
            .Returns(listaFuncionarios);

        var result = _controller.BuscarFuncionarios();

        result.Sucesso.Should().BeTrue();
        result.Mensagens.Should().BeNull();
        result.Dados.Should().NotBeNull();
        result.Dados.Count.Should().Be(listaFuncionarios.Count);
        result.Dados.ForEach(x => x.Should().BeOfType<FuncionarioDto>());
        for (var i =0; i< listaFuncionarios.Count; i++)
        {
            result.Dados[i].Should().BeEquivalentTo(listaFuncionarios[i]);
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

        var result = () => _controller.EditarFuncionario(request, funcionarioId);

        result.Should().NotThrow<ErroValidacaoException>();
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
        
        Action result = () => _controller.EditarFuncionario(request, funcionarioId);
        
        result.Should().Throw<NaoEncontradoException>();
        _funcionarioServiceMock.Verify(x => x.Editar(It.Is<long>(x => x == funcionarioId), It.IsAny<FuncionarioDto>()), Times.Once);
    }

    [Fact]
    public void ExcluirFuncionario_ComDadosValidos_DeveRealizarOperacao()
    {
        var funcionarioId = 1;
        _funcionarioServiceMock.Setup(x => x.Excluir(It.Is<long>(x => x == funcionarioId)));

        var result = () => _controller.ExcluirFuncionario(funcionarioId);
        
        result.Should().NotThrow<NaoEncontradoException>();
        _funcionarioServiceMock.Verify(x => x.Excluir(It.Is<long>(x => x == funcionarioId)), Times.Once);
    }
    
    [Fact]
    public void ExcluirFuncionario_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var funcionarioId = 1;
        _funcionarioServiceMock.Setup(x => x.Excluir(It.Is<long>(x => x == funcionarioId)))
            .Throws(new NaoEncontradoException("Funcionario não encontrado"));

        Action result = () => _controller.ExcluirFuncionario(funcionarioId);
        
        result.Should().Throw<NaoEncontradoException>();
        _funcionarioServiceMock.Verify(x => x.Excluir(It.Is<long>(x => x == funcionarioId)), Times.Once);
    }
}