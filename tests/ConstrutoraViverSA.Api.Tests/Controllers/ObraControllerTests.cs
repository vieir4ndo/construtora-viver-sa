using System;
using System.Linq;
using AutoFixture;
using AutoMapper;
using ConstrutoraViverSA.Api.Controllers;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Application.Interfaces;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace ConstrutoraViverSA.Api.Tests.Controllers;

public class ObraControllerTests
{
    private readonly Mock<IObraService> _obraServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ObraController _controller;
    private readonly Fixture _fixture = new Fixture();

    public ObraControllerTests()
    {
        _obraServiceMock = new Mock<IObraService>();
        _mapperMock = new Mock<IMapper>();
        _controller = new ObraController(_obraServiceMock.Object, _mapperMock.Object);
    }

    [Fact]
    public void CadastrarObra_ComDadosValidos_DeveRealizarOperacao()
    {
        var request = _fixture.Build<ObraRequest>()
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        var dto = _fixture.Build<ObraDto>()
            .Create();
        _mapperMock.Setup(x => x.Map<ObraDto>(It.Is<ObraRequest>(x => x == request))).Returns(dto);
        _obraServiceMock.Setup(x => x.Adicionar(It.Is<ObraDto>(x => x == dto)));

        var resultado = _controller.CadastrarObra(request);

        resultado.Sucesso.Should().BeTrue();
        resultado.Dados.Should().BeNull();
        resultado.Mensagens.Should().BeNull();
        _obraServiceMock.Verify(X => X.Adicionar(It.Is<ObraDto>(x => x == dto)), Times.Once);
        _mapperMock.Verify(x => x.Map<ObraDto>(It.Is<ObraRequest>(x => x == request)), Times.Once);
    }
    
    [Fact]
    public void CadastrarObra_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var request = _fixture.Build<ObraRequest>()
            .Without(x => x.Valor)
            .Create();
     
        var resultado = () => _controller.CadastrarObra(request);
        
        resultado.Should().Throw<ErroValidacaoException>();
    }

    [Fact]
    public void BuscarObra_ComDadosValidos_DeveRealizarOperacao()
    {
        var obraId = 1;
        var obra = _fixture.Create<ObraDto>();
        _obraServiceMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == obraId))).Returns(obra);

        var resultado = _controller.BuscarObra(obraId);

        resultado.Sucesso.Should().BeTrue();
        resultado.Mensagens.Should().BeNull();
        resultado.Dados.Should().NotBeNull();
        resultado.Dados.First().Should().BeOfType<ObraDto>();
        resultado.Dados.First().Should().BeEquivalentTo(obra);
        _obraServiceMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == obraId)), Times.Once);
    }
    
    [Fact]
    public void BuscarObra_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var obraId = 1;
        _obraServiceMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == obraId)))
            .Throws(new NaoEncontradoException("Obra não encontrado"));

        var resultado = () => _controller.BuscarObra(obraId);

        resultado.Should().Throw<NaoEncontradoException>();
        _obraServiceMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == obraId)), Times.Once);
    }

    [Fact]
    public void BuscarObras_ComDadosValidos_DeveRealizarOperacao()
    {
        var listaObras = _fixture.CreateMany<ObraDto>().ToList();
        _obraServiceMock.Setup(x => x.BuscarTodos())
            .Returns(listaObras);

        var resultado = _controller.BuscarObras();

        resultado.Sucesso.Should().BeTrue();
        resultado.Mensagens.Should().BeNull();
        resultado.Dados.Should().NotBeNull();
        resultado.Dados.Count.Should().Be(listaObras.Count);
        resultado.Dados.ForEach(x => x.Should().BeOfType<ObraDto>());
        for (var i =0; i< listaObras.Count; i++)
        {
            resultado.Dados[i].Should().BeEquivalentTo(listaObras[i]);
        }
        _obraServiceMock.Verify(x => x.BuscarTodos(), Times.Once);
    }

    [Fact]
    public void EditarObra_ComDadosValidos_DeveRealizarOperacao()
    {
        var obraId = 1;
        var request = _fixture.Build<EditarObraRequest>()
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        var dto = _fixture.Build<ObraDto>()
            .Create();
        _mapperMock.Setup(x => x.Map<ObraDto>(It.Is<EditarObraRequest>(x => x == request))).Returns(dto);
        _obraServiceMock.Setup(x => x.Editar(It.Is<long>(x => x == obraId),It.Is<ObraDto>(x => x == dto)));

        var resultado = _controller.EditarObra(request, obraId);

        resultado.Sucesso.Should().BeTrue();
        resultado.Dados.Should().BeNull();
        resultado.Mensagens.Should().BeNull();
        _obraServiceMock.Verify(X => X.Editar(It.Is<long>(x => x == obraId),It.Is<ObraDto>(x => x == dto)), Times.Once);
        _mapperMock.Verify(x => x.Map<ObraDto>(It.Is<EditarObraRequest>(x => x == request)), Times.Once);
    }

    [Fact]
    public void EditarObra_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var obraId = 1;
        var request = _fixture.Build<EditarObraRequest>()
            .With(x => x.PrazoConclusao, DateTime.Today.AddDays(1))
            .Create();
        _obraServiceMock.Setup(x => x.Editar(It.Is<long>(x => x == obraId), It.IsAny<ObraDto>()))
            .Throws(new NaoEncontradoException("Obra não encontrado"));
        
        var resultado = () => _controller.EditarObra(request, obraId);
        
        resultado.Should().Throw<NaoEncontradoException>();
        _obraServiceMock.Verify(x => x.Editar(It.Is<long>(x => x == obraId), It.IsAny<ObraDto>()), Times.Once);
    }

    [Fact]
    public void ExcluirObra_ComDadosValidos_DeveRealizarOperacao()
    {
        var obraId = 1;
        _obraServiceMock.Setup(x => x.Excluir(It.Is<long>(x => x == obraId)));

        var resultado = _controller.ExcluirObra(obraId);
        
        resultado.Sucesso.Should().BeTrue();
        resultado.Dados.Should().BeNull();
        resultado.Mensagens.Should().BeNull();
        _obraServiceMock.Verify(x => x.Excluir(It.Is<long>(x => x == obraId)), Times.Once);
    }
    
    [Fact]
    public void ExcluirObra_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var obraId = 1;
        _obraServiceMock.Setup(x => x.Excluir(It.Is<long>(x => x == obraId)))
            .Throws(new NaoEncontradoException("Obra não encontrado"));

        var resultado = () => _controller.ExcluirObra(obraId);
        
        resultado.Should().Throw<NaoEncontradoException>();
        _obraServiceMock.Verify(x => x.Excluir(It.Is<long>(x => x == obraId)), Times.Once);
    }

    [Fact]
    public void AlocarFuncionarioNaObra_ComDadosValidos_DeveRealizarOperacao()
    {
        var obraId = 1;
        var funcionarioId = 1;
        _obraServiceMock.Setup(x => x.AlocarFuncionario(It.Is<long>(x => x == obraId), It.Is<long>(x => x == funcionarioId)));

        var resultado = _controller.AlocarFuncionarioNaObra(obraId, funcionarioId);
        
        resultado.Sucesso.Should().BeTrue();
        resultado.Dados.Should().BeNull();
        resultado.Mensagens.Should().BeNull();
        _obraServiceMock.Verify(x => x.AlocarFuncionario(It.Is<long>(x => x == obraId), It.Is<long>(x => x == funcionarioId)), Times.Once);
    }
    
    [Fact]
    public void AlocarFuncionarioNaObra_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var obraId = 1;
        var funcionarioId = 1;
        _obraServiceMock.Setup(x => x.AlocarFuncionario(It.Is<long>(x => x == obraId), It.Is<long>(x => x == funcionarioId)))
            .Throws( new NaoEncontradoException("Obra não encontrado"));

        var resultado = () => _controller.AlocarFuncionarioNaObra(obraId, funcionarioId);
        
        resultado.Should().Throw<NaoEncontradoException>();
        _obraServiceMock.Verify(x => x.AlocarFuncionario(It.Is<long>(x => x == obraId), It.Is<long>(x => x == funcionarioId)), Times.Once);
    }
    
    [Fact]
    public void DesalocarFuncionarioNaObra_ComDadosValidos_DeveRealizarOperacao()
    {
        var obraId = 1;
        var funcionarioId = 1;
        _obraServiceMock.Setup(x => x.DesalocarFuncionario(It.Is<long>(x => x == obraId), It.Is<long>(x => x == funcionarioId)));

        var resultado = _controller.DesalocarFuncionarioNaObra(obraId, funcionarioId);
        
        resultado.Sucesso.Should().BeTrue();
        resultado.Dados.Should().BeNull();
        resultado.Mensagens.Should().BeNull();
        _obraServiceMock.Verify(x => x.DesalocarFuncionario(It.Is<long>(x => x == obraId), It.Is<long>(x => x == funcionarioId)), Times.Once);
    }
    
    [Fact]
    public void DesalocarFuncionarioNaObra_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var obraId = 1;
        var funcionarioId = 1;
        _obraServiceMock.Setup(x => x.DesalocarFuncionario(It.Is<long>(x => x == obraId), It.Is<long>(x => x == funcionarioId)))
            .Throws( new NaoEncontradoException("Obra não encontrado"));

        var resultado = () => _controller.DesalocarFuncionarioNaObra(obraId, funcionarioId);
        
        resultado.Should().Throw<NaoEncontradoException>();
        _obraServiceMock.Verify(x => x.DesalocarFuncionario(It.Is<long>(x => x == obraId), It.Is<long>(x => x == funcionarioId)), Times.Once);
    }
    
    [Fact]
    public void GerenciarMaterialNaObra_ComDadosValidos_DeveRealizarOperacao()
    {
        var obraId = 1;
        var quantidade = 1;
        var materialId = 1;
        var dto = _fixture.Build<EntradaSaidaMaterialDto>()
            .With(x => x.Operacao, EntradaSaida.Entrada)
            .With(x => x.Quantidade, quantidade)
            .Create();
        var request = _fixture.Build<EntradaSaidaMaterialRequest>()
            .With(x => x.Operacao, EntradaSaida.Entrada)
            .With(x => x.Quantidade, quantidade)
            .Create();
        _obraServiceMock.Setup(x => x.GerenciarMaterial(It.Is<EntradaSaidaMaterialDto>(x => x == dto), It.Is<long>(x => x == obraId), It.Is<long>(x => x == materialId)));
        _mapperMock.Setup(x => x.Map<EntradaSaidaMaterialDto>(It.Is<EntradaSaidaMaterialRequest>(x => x == request))).Returns(dto);

        var resultado = _controller.GerenciarMaterialNaObra(request, obraId, materialId);
        
        resultado.Sucesso.Should().BeTrue();
        resultado.Dados.Should().BeNull();
        resultado.Mensagens.Should().BeNull();
        _mapperMock.Verify(x => x.Map<EntradaSaidaMaterialDto>(It.Is<EntradaSaidaMaterialRequest>(x => x == request)), Times.Once);
        _obraServiceMock.Verify(x => x.GerenciarMaterial(It.Is<EntradaSaidaMaterialDto>(x => x == dto), It.Is<long>(x => x == obraId), It.Is<long>(x => x == materialId)), Times.Once);
    }
    
    [Fact]
    public void GerenciarMaterialNaObra_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var obraId = 1;
        var quantidade = 1;
        var materialId = 1;
        var dto = _fixture.Build<EntradaSaidaMaterialDto>()
            .With(x => x.Operacao, EntradaSaida.Entrada)
            .With(x => x.Quantidade, quantidade)
            .Create();
        var request = _fixture.Build<EntradaSaidaMaterialRequest>()
            .With(x => x.Operacao, EntradaSaida.Entrada)
            .With(x => x.Quantidade, quantidade)
            .Create();
        _obraServiceMock.Setup(x => x.GerenciarMaterial(It.Is<EntradaSaidaMaterialDto>(x => x == dto), It.Is<long>(x => x == obraId), It.Is<long>(x => x == materialId)))
            .Throws( new NaoEncontradoException("Obra não encontrado"));;
        _mapperMock.Setup(x => x.Map<EntradaSaidaMaterialDto>(It.Is<EntradaSaidaMaterialRequest>(x => x == request))).Returns(dto);

        var resultado = () => _controller.GerenciarMaterialNaObra(request, obraId, materialId);
        
        resultado.Should().Throw<NaoEncontradoException>();
        _obraServiceMock.Verify(x => x.GerenciarMaterial(It.Is<EntradaSaidaMaterialDto>(x => x == dto), It.Is<long>(x => x == obraId), It.Is<long>(x => x == materialId)), Times.Once);
        _mapperMock.Verify(x => x.Map<EntradaSaidaMaterialDto>(It.Is<EntradaSaidaMaterialRequest>(x => x == request)), Times.Once);
    }
}