using System;
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

public class OrcamentoControllerTests
{
    private readonly Mock<IOrcamentoService> _orcamentoServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly OrcamentoController _controller;
    private readonly Fixture _fixture = new Fixture();

    public OrcamentoControllerTests()
    {
        _orcamentoServiceMock = new Mock<IOrcamentoService>();
        _mapperMock = new Mock<IMapper>();
        _controller = new OrcamentoController(_orcamentoServiceMock.Object, _mapperMock.Object);
    }

    [Fact]
    public void CadastrarOrcamento_ComDadosValidos_DeveRealizarOperacao()
    {
        var request = _fixture.Build<OrcamentoRequest>()
            .With(x => x.DataEmissao, DateTime.Today)
            .With(x => x.DataValidade, DateTime.Today.AddDays(1))
            .Create();
        var dto = _fixture.Build<OrcamentoDto>()
            .Create();
        _mapperMock.Setup(x => x.Map<OrcamentoDto>(It.Is<OrcamentoRequest>(x => x == request))).Returns(dto);
        _orcamentoServiceMock.Setup(x => x.Adicionar(It.Is<OrcamentoDto>(x => x == dto)));

        var resultado = _controller.CadastrarOrcamento(request);

        resultado.Sucesso.Should().BeTrue();
        resultado.Dados.Should().BeNull();
        resultado.Mensagens.Should().BeNull();
        _orcamentoServiceMock.Verify(X => X.Adicionar(It.Is<OrcamentoDto>(x => x == dto)), Times.Once);
        _mapperMock.Verify(x => x.Map<OrcamentoDto>(It.Is<OrcamentoRequest>(x => x == request)), Times.Once);
    }
    
    [Fact]
    public void CadastrarOrcamento_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var request = _fixture.Build<OrcamentoRequest>()
            .Without(x => x.Valor)
            .Create();
     
        var resultado = () => _controller.CadastrarOrcamento(request);
        
        resultado.Should().Throw<ErroValidacaoException>();
       }

    [Fact]
    public void BuscarOrcamento_ComDadosValidos_DeveRealizarOperacao()
    {
        var orcamentoId = 1;
        var orcamento = _fixture.Create<OrcamentoDto>();
        _orcamentoServiceMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == orcamentoId))).Returns(orcamento);

        var resultado = _controller.BuscarOrcamento(orcamentoId);

        resultado.Sucesso.Should().BeTrue();
        resultado.Mensagens.Should().BeNull();
        resultado.Dados.Should().NotBeNull();
        resultado.Dados.First().Should().BeOfType<OrcamentoDto>();
        resultado.Dados.First().Should().BeEquivalentTo(orcamento);
        _orcamentoServiceMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == orcamentoId)), Times.Once);
    }
    
    [Fact]
    public void BuscarOrcamento_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var orcamentoId = 1;
        _orcamentoServiceMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == orcamentoId)))
            .Throws(new NaoEncontradoException("Orcamento não encontrado"));

        var resultado = () => _controller.BuscarOrcamento(orcamentoId);

        resultado.Should().Throw<NaoEncontradoException>();
        _orcamentoServiceMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == orcamentoId)), Times.Once);
    }

    [Fact]
    public void BuscarOrcamentos_ComDadosValidos_DeveRealizarOperacao()
    {
        var listaOrcamentos = _fixture.CreateMany<OrcamentoDto>().ToList();
        _orcamentoServiceMock.Setup(x => x.BuscarTodos())
            .Returns(listaOrcamentos);

        var resultado = _controller.BuscarOrcamentos();

        resultado.Sucesso.Should().BeTrue();
        resultado.Mensagens.Should().BeNull();
        resultado.Dados.Should().NotBeNull();
        resultado.Dados.Count.Should().Be(listaOrcamentos.Count);
        resultado.Dados.ForEach(x => x.Should().BeOfType<OrcamentoDto>());
        for (var i =0; i< listaOrcamentos.Count; i++)
        {
            resultado.Dados[i].Should().BeEquivalentTo(listaOrcamentos[i]);
        }
        _orcamentoServiceMock.Verify(x => x.BuscarTodos(), Times.Once);
    }

    [Fact]
    public void EditarOrcamento_ComDadosValidos_DeveRealizarOperacao()
    {
        var orcamentoId = 1;
        var request = _fixture.Build<OrcamentoRequest>()
            .With(x => x.DataEmissao, DateTime.Today)
            .With(x => x.DataValidade, DateTime.Today.AddDays(1))
            .Create();
        var dto = _fixture.Build<OrcamentoDto>()
            .Create();
        _mapperMock.Setup(x => x.Map<OrcamentoDto>(It.Is<OrcamentoRequest>(x => x == request))).Returns(dto);
        _orcamentoServiceMock.Setup(x => x.Editar(It.Is<long>(x => x == orcamentoId),It.Is<OrcamentoDto>(x => x == dto)));

        var resultado = _controller.EditarOrcamento(request, orcamentoId);

        resultado.Sucesso.Should().BeTrue();
        resultado.Dados.Should().BeNull();
        resultado.Mensagens.Should().BeNull();
        _orcamentoServiceMock.Verify(X => X.Editar(It.Is<long>(x => x == orcamentoId),It.Is<OrcamentoDto>(x => x == dto)), Times.Once);
        _mapperMock.Verify(x => x.Map<OrcamentoDto>(It.Is<OrcamentoRequest>(x => x == request)), Times.Once);
    }

    [Fact]
    public void EditarOrcamento_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var orcamentoId = 1;
        var request = _fixture.Build<OrcamentoRequest>()
            .With(x => x.DataEmissao, DateTime.Today)
            .With(x => x.DataValidade, DateTime.Today.AddDays(1))
            .Create();
        _orcamentoServiceMock.Setup(x => x.Editar(It.Is<long>(x => x == orcamentoId), It.IsAny<OrcamentoDto>()))
            .Throws(new NaoEncontradoException("Orcamento não encontrado"));
        
        var resultado = () => _controller.EditarOrcamento(request, orcamentoId);
        
        resultado.Should().Throw<NaoEncontradoException>();
        _orcamentoServiceMock.Verify(x => x.Editar(It.Is<long>(x => x == orcamentoId), It.IsAny<OrcamentoDto>()), Times.Once);
    }

    [Fact]
    public void ExcluirOrcamento_ComDadosValidos_DeveRealizarOperacao()
    {
        var orcamentoId = 1;
        _orcamentoServiceMock.Setup(x => x.Excluir(It.Is<long>(x => x == orcamentoId)));

        var resultado = _controller.ExcluirOrcamento(orcamentoId);
        
        resultado.Sucesso.Should().BeTrue();
        resultado.Dados.Should().BeNull();
        resultado.Mensagens.Should().BeNull();
        _orcamentoServiceMock.Verify(x => x.Excluir(It.Is<long>(x => x == orcamentoId)), Times.Once);
    }
    
    [Fact]
    public void ExcluirOrcamento_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var orcamentoId = 1;
        _orcamentoServiceMock.Setup(x => x.Excluir(It.Is<long>(x => x == orcamentoId)))
            .Throws(new NaoEncontradoException("Orcamento não encontrado"));

        var resultado = () => _controller.ExcluirOrcamento(orcamentoId);
        
        resultado.Should().Throw<NaoEncontradoException>();
        _orcamentoServiceMock.Verify(x => x.Excluir(It.Is<long>(x => x == orcamentoId)), Times.Once);
    }
}