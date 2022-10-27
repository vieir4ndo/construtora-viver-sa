using System;
using System.Linq;
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

namespace ConstrutoraViverSA.Application.Tests.Services;

public class OrcamentoServiceTests
{
    private readonly Mock<IOrcamentoRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Fixture _fixture = new Fixture();
    private readonly OrcamentoService _service; 

    public OrcamentoServiceTests()
    {
        _repositoryMock = new Mock<IOrcamentoRepository>();
        _mapperMock = new Mock<IMapper>();
        _service = new OrcamentoService(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public void BuscarTodos_ComDadosValidos_DeveRealizarOperacao()
    {
        var orcamentos = _fixture.CreateMany<Orcamento>().ToList();
        var orcamentosDto = _fixture.CreateMany<OrcamentoDto>(3).ToList();
        _repositoryMock.Setup(x => x.BuscarTodos()).Returns(orcamentos);
        _mapperMock.SetupSequence(x => x.Map<OrcamentoDto>(It.IsAny<Orcamento>()))
            .Returns(orcamentosDto[0])
            .Returns(orcamentosDto[1])
            .Returns(orcamentosDto[2]);

        var resultado = _service.BuscarTodos();

        resultado.Count.Should().Be(orcamentos.Count);
        resultado.Should().BeEquivalentTo(orcamentosDto);
        _repositoryMock.Verify(x => x.BuscarTodos(), Times.Once);
        _mapperMock.Verify(x => x.Map<OrcamentoDto>(It.IsAny<Orcamento>()), Times.Exactly(3));
    }

    [Fact]
    public void BuscarEntidadePorId_ComDadosValidos_DeveRealizarOperacao()
    {
        var orcamentoId = 1;
        var orcamento = _fixture.Build<Orcamento>()
            .With(x => x.Id, orcamentoId)
            .Create();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == orcamentoId)))
            .Returns(orcamento);

        var resultado = _service.BuscarEntidadePorId(orcamentoId);

        resultado.Should().NotBeNull();
        resultado.Should().BeOfType<Orcamento>();
        resultado.Should().BeEquivalentTo(orcamento);
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == orcamentoId)), Times.Once);
    }
    
    [Fact]
    public void BuscarEntidadePorId_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var orcamentoId = 1;
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == orcamentoId)))
            .Returns((Orcamento)null);

        var resultado = () => _service.BuscarEntidadePorId(orcamentoId);

        resultado.Should().Throw<NaoEncontradoException>();
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == orcamentoId)), Times.Once);
    }
    
    [Fact]
    public void BuscarPorId_ComDadosValidos_DeveRealizarOperacao()
    {
        var orcamentoId = 1;
        var orcamento = _fixture.Build<Orcamento>()
            .With(x => x.Id, orcamentoId)
            .Create();
        var orcamentoDto = _fixture.Create<OrcamentoDto>();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == orcamentoId)))
            .Returns(orcamento);
        _mapperMock.Setup(x => x.Map<OrcamentoDto>(It.Is<Orcamento>(x => x == orcamento)))
            .Returns(orcamentoDto);

        var resultado = _service.BuscarPorId(orcamentoId);

        resultado.Should().NotBeNull();
        resultado.Should().BeOfType<OrcamentoDto>();
        resultado.Should().BeEquivalentTo(orcamentoDto);
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == orcamentoId)), Times.Once);
        _mapperMock.Verify(x => x.Map<OrcamentoDto>(It.Is<Orcamento>(x => x == orcamento)), Times.Once);
    }
    
    [Fact]
    public void BuscarPorId_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var orcamentoId = 1;
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == orcamentoId)))
            .Returns((Orcamento)null);

        var resultado = () => _service.BuscarPorId(orcamentoId);

        resultado.Should().Throw<NaoEncontradoException>();
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == orcamentoId)), Times.Once);
    }

    [Fact]
    public void Adicionar_ComDadosValidos_DeveRealizarOperacao()
    {
        var dto = _fixture.Build<OrcamentoDto>()
            .With(x => x.DataEmissao, DateTime.Today)
            .With(x => x.DataValidade, DateTime.Today.AddDays(1))
            .Create();
        _repositoryMock.Setup(x => x.Adicionar(It.IsAny<Orcamento>()));

        var resultado = () => _service.Adicionar(dto);

        resultado.Should().NotThrow<OrcamentoInvalidoException>();
        _repositoryMock.Verify(x => x.Adicionar(It.IsAny<Orcamento>()), Times.Once);
    }
    
    [Fact]
    public void Adicionar_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var dto = _fixture.Build<OrcamentoDto>()
            .With(x => x.DataEmissao, DateTime.Today)
            .With(x => x.DataValidade, DateTime.Today.AddDays(-1))
            .Create();

        var resultado = () => _service.Adicionar(dto);

        resultado.Should().Throw<OrcamentoInvalidoException>();
    }
    
    [Fact]
    public void Editar_ComDadosValidos_DeveRealizarOperacao()
    {
        var orcamentoId = 1;
        var dto = _fixture.Build<OrcamentoDto>()
            .With(x => x.DataEmissao, DateTime.Today)
            .With(x => x.DataValidade, DateTime.Today.AddDays(1))
            .Create();
        var orcamento = _fixture.Build<Orcamento>()
            .With(x => x.Id, orcamentoId)
            .Create();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == orcamentoId))).Returns(orcamento);
        _repositoryMock.Setup(x => x.Editar(It.IsAny<Orcamento>()));

        var resultado = () => _service.Editar(orcamentoId, dto);

        resultado.Should().NotThrow<NaoEncontradoException>();
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == orcamentoId)), Times.Once);
        _repositoryMock.Verify(x => x.Editar(It.IsAny<Orcamento>()), Times.Once);
    }
    
    [Fact]
    public void Editar_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var orcamentoId = 1;
        var dto = _fixture.Build<OrcamentoDto>()
            .With(x => x.DataEmissao, DateTime.Today)
            .With(x => x.DataValidade, DateTime.Today.AddDays(-1))
            .Create();
        var orcamento = _fixture.Build<Orcamento>()
            .With(x => x.Id, orcamentoId)
            .Create();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == orcamentoId))).Returns(orcamento);
        _repositoryMock.Setup(x => x.Editar(It.IsAny<Orcamento>()));

        var resultado = () => _service.Editar(orcamentoId, dto);
        
        resultado.Should().NotThrow<NaoEncontradoException>();
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == orcamentoId)), Times.Once);
        _repositoryMock.Verify(x => x.Editar(It.IsAny<Orcamento>()), Times.Once); 
    }
    
    [Fact]
    public void Excluir_ComDadosValidos_DeveRealizarOperacao()
    {
        var orcamentoId = 1;
        var orcamento = _fixture.Build<Orcamento>()
            .With(x => x.Id, orcamentoId)
            .Create();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == orcamentoId))).Returns(orcamento);
        _repositoryMock.Setup(x => x.Excluir(It.Is<Orcamento>(x => x == orcamento)));

        var resultado = () => _service.Excluir(orcamentoId);

        resultado.Should().NotThrow<NaoEncontradoException>();
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == orcamentoId)), Times.Once);
        _repositoryMock.Verify(x => x.Excluir(It.Is<Orcamento>(x => x == orcamento)), Times.Once);
    }
    
    [Fact]
    public void Excluir_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var orcamentoId = 1;
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == orcamentoId)))
            .Returns((Orcamento)null);

        var resultado = () => _service.Excluir(orcamentoId);

        resultado.Should().Throw<NaoEncontradoException>();
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == orcamentoId)), Times.Once);
        _repositoryMock.Verify(x => x.Excluir(It.IsAny<Orcamento>()), Times.Never);
    }

}