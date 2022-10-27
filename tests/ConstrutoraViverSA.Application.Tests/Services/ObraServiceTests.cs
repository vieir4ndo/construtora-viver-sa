using System.Linq;
using AutoFixture;
using AutoMapper;
using ConstrutoraViverSA.Application.Interfaces;
using ConstrutoraViverSA.Application.Services;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Exceptions;
using ConstrutoraViverSA.Repository.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace ConstrutoraViverSA.Application.Tests.Services;

public class ObraServiceTests
{
    private readonly Mock<IObraRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Fixture _fixture = new Fixture();
    private readonly ObraService _service; 
    private readonly Mock<IFuncionarioService> _funcionarioServiceMock;
    private readonly Mock<IMaterialService> _materialServiceMock;
    private readonly Mock<IOrcamentoService> _orcamentoServiceMock;
    private readonly Mock<IObraParaObraDto> _obraParaObraDtoServiceMock;

    public ObraServiceTests()
    {
        _repositoryMock = new Mock<IObraRepository>();
        _mapperMock = new Mock<IMapper>();
        _funcionarioServiceMock = new Mock<IFuncionarioService>();
        _materialServiceMock = new Mock<IMaterialService>();
        _orcamentoServiceMock = new Mock<IOrcamentoService>();
        _obraParaObraDtoServiceMock = new Mock<IObraParaObraDto>();
        _service = new ObraService(_repositoryMock.Object, _orcamentoServiceMock.Object, _funcionarioServiceMock.Object, _materialServiceMock.Object,_mapperMock.Object, _obraParaObraDtoServiceMock.Object);
    }

    [Fact]
    public void BuscarTodos_ComDadosValidos_DeveRealizarOperacao()
    {
        var obras = _fixture.CreateMany<Obra>().ToList();
        var obrasDto = _fixture.CreateMany<ObraDto>(3).ToList();
        _repositoryMock.Setup(x => x.BuscarTodos()).Returns(obras);
        _obraParaObraDtoServiceMock.SetupSequence(x => x.Mapear(It.IsAny<Obra>()))
            .Returns(obrasDto[0])
            .Returns(obrasDto[1])
            .Returns(obrasDto[2]);

        var resultado = _service.BuscarTodos();

        resultado.Count.Should().Be(obras.Count);
        resultado.Should().BeEquivalentTo(obrasDto);
        _repositoryMock.Verify(x => x.BuscarTodos(), Times.Once);
        _obraParaObraDtoServiceMock.Verify(x => x.Mapear(It.IsAny<Obra>()), Times.Exactly(3));
    }

    [Fact]
    public void BuscarEntidadePorId_ComDadosValidos_DeveRealizarOperacao()
    {
        var obraId = 1;
        var obra = _fixture.Build<Obra>()
            .Create();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == obraId)))
            .Returns(obra);

        var resultado = _service.BuscarEntidadePorId(obraId);

        resultado.Should().NotBeNull();
        resultado.Should().BeOfType<Obra>();
        resultado.Should().BeEquivalentTo(obra);
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == obraId)), Times.Once);
    }
    
    [Fact]
    public void BuscarEntidadePorId_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var obraId = 1;
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == obraId)))
            .Returns((Obra)null);

        var resultado = () => _service.BuscarEntidadePorId(obraId);

        resultado.Should().Throw<NaoEncontradoException>();
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == obraId)), Times.Once);
    }
    
    [Fact]
    public void BuscarPorId_ComDadosValidos_DeveRealizarOperacao()
    {
        var obraId = 1;
        var obra = _fixture.Build<Obra>()
            .Create();
        var obraDto = _fixture.Create<ObraDto>();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == obraId)))
            .Returns(obra);
        _obraParaObraDtoServiceMock.Setup(x => x.Mapear(It.Is<Obra>(x => x == obra)))
            .Returns(obraDto);

        var resultado = _service.BuscarPorId(obraId);

        resultado.Should().NotBeNull();
        resultado.Should().BeOfType<ObraDto>();
        resultado.Should().BeEquivalentTo(obraDto);
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == obraId)), Times.Once);
        _obraParaObraDtoServiceMock.Verify(x => x.Mapear(It.Is<Obra>(x => x == obra)), Times.Once);
    }
    
    [Fact]
    public void BuscarPorId_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var obraId = 1;
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == obraId)))
            .Returns((Obra)null);

        var resultado = () => _service.BuscarPorId(obraId);

        resultado.Should().Throw<NaoEncontradoException>();
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == obraId)), Times.Once);
    }

    [Fact]
    public void Adicionar_ComDadosValidos_DeveRealizarOperacao()
    {
        var orcamentoId = 1;
        var dto = _fixture.Build<ObraDto>()
            .With(x => x.OrcamentoId, orcamentoId)
            .Without(x => x.Funcionarios)
            .Without(x => x.Materiais)
            .Create();
        var orcamento = _fixture.Build<Orcamento>()
            .With(x => x.Id, orcamentoId)
            .Create();
        _orcamentoServiceMock.Setup(x => x.BuscarEntidadePorId(It.Is<long>(x => x == orcamentoId))).Returns(orcamento);

        var resultado = () => _service.Adicionar(dto);

        resultado.Should().NotThrow<ObraInvalidaException>();
        _repositoryMock.Verify(x => x.Adicionar(It.IsAny<Obra>()), Times.Once);
    }
    
    [Fact]
    public void Adicionar_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var orcamentoId = 1;
        var dto = _fixture.Build<ObraDto>()
            .With(x => x.OrcamentoId, orcamentoId)
            .Without(x => x.Nome)
            .Without(x => x.Funcionarios)
            .Without(x => x.Materiais)
            .Create();
        var orcamento = _fixture.Build<Orcamento>()
            .With(x => x.Id, orcamentoId)
            .Create();
        _orcamentoServiceMock.Setup(x => x.BuscarEntidadePorId(It.Is<long>(x => x == orcamentoId))).Returns(orcamento);

        var resultado = () => _service.Adicionar(dto);

        resultado.Should().Throw<ObraInvalidaException>();
    }

    [Fact]
    public void Adicionar_ComDadosInvalidosEOrcamentoInexistente_NaoDeveRealizarOperacao()
    {
        
    }
    
    [Fact]
    public void Adicionar_ComDadosInvalidosEFuncionarioInexistente_NaoDeveRealizarOperacao()
    {
        
    }
    
    [Fact]
    public void Adicionar_ComDadosInvalidosEMaterialInexistente_NaoDeveRealizarOperacao()
    {
        
    }

    [Fact]
    public void Editar_ComDadosValidos_DeveRealizarOperacao()
    {
       
    }
    
    [Fact]
    public void Editar_ComDadosValidosEOrcamento_DeveRealizarOperacao()
    {
       
    }
    
    [Fact]
    public void Editar_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        
    }
    
    [Fact]
    public void Editar_ComDadosInvalidosEOrcamentoInexistente_NaoDeveRealizarOperacao()
    {
        
    }
    
    [Fact]
    public void Excluir_ComDadosValidos_DeveRealizarOperacao()
    {
        var obraId = 1;
        var obra = _fixture.Build<Obra>()
            .Create();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == obraId))).Returns(obra);
        _repositoryMock.Setup(x => x.Excluir(It.Is<Obra>(x => x == obra)));

        var resultado = () => _service.Excluir(obraId);

        resultado.Should().NotThrow<NaoEncontradoException>();
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == obraId)), Times.Once);
        _repositoryMock.Verify(x => x.Excluir(It.Is<Obra>(x => x == obra)), Times.Once);
    }
    
    [Fact]
    public void Excluir_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var obraId = 1;
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == obraId)))
            .Returns((Obra)null);

        var resultado = () => _service.Excluir(obraId);

        resultado.Should().Throw<NaoEncontradoException>();
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == obraId)), Times.Once);
        _repositoryMock.Verify(x => x.Excluir(It.IsAny<Obra>()), Times.Never);
    }

    [Fact]
    public void GerenciarMaterial_ComDadosValidos_DeveRealizarOperacao()
    {
        
    }
    
    [Fact]
    public void GerenciarMaterial_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        
    }
    
    [Fact]
    public void GerenciarMaterial_ComDadosInvalidosEMaterialInexistente_NaoDeveRealizarOperacao()
    {
        
    }
    
    [Fact]
    public void AlocarFuncionario_ComDadosValidos_DeveRealizarOperacao()
    {
        
    }
    
    [Fact]
    public void AlocarFuncionario_ComDadosInvalidosEFuncionarioInexistente_NaoDeveRealizarOperacao()
    {
        
    }
    
    [Fact]
    public void DesalocarFuncionario_ComDadosValidos_DeveRealizarOperacao()
    {
        
    }
    
    [Fact]
    public void DesalocarFuncionario_ComDadosInvalidosEFuncionarioInexistente_NaoDeveRealizarOperacao()
    {
        
    }
}