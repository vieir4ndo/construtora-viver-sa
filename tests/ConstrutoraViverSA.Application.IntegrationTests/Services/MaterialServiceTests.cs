using AutoFixture;
using AutoMapper;
using ConstrutoraViverSA.Application.Services;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;
using ConstrutoraViverSA.Repository.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace ConstrutoraViverSA.Application.IntegrationTests.Services;

public class MaterialServiceTests
{
    private readonly Mock<IMaterialRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Fixture _fixture = new Fixture();
    private readonly MaterialService _service; 

    public MaterialServiceTests()
    {
        _repositoryMock = new Mock<IMaterialRepository>();
        _mapperMock = new Mock<IMapper>();
        _service = new MaterialService(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public void BuscarTodos_ComDadosValidos_DeveRealizarOperacao()
    {
        var materials = _fixture.CreateMany<Material>().ToList();
        var materialsDto = _fixture.CreateMany<MaterialDto>(3).ToList();
        _repositoryMock.Setup(x => x.BuscarTodos()).Returns(materials);
        _mapperMock.SetupSequence(x => x.Map<MaterialDto>(It.IsAny<Material>()))
            .Returns(materialsDto[0])
            .Returns(materialsDto[1])
            .Returns(materialsDto[2]);

        var resultado = _service.BuscarTodos();

        resultado.Count.Should().Be(materials.Count);
        resultado.Should().BeEquivalentTo(materialsDto);
        _repositoryMock.Verify(x => x.BuscarTodos(), Times.Once);
        _mapperMock.Verify(x => x.Map<MaterialDto>(It.IsAny<Material>()), Times.Exactly(3));
    }

    [Fact]
    public void BuscarEntidadePorId_ComDadosValidos_DeveRealizarOperacao()
    {
        var materialId = 1;
        var material = _fixture.Build<Material>()
            .Create();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == materialId)))
            .Returns(material);

        var resultado = _service.BuscarEntidadePorId(materialId);

        resultado.Should().NotBeNull();
        resultado.Should().BeOfType<Material>();
        resultado.Should().BeEquivalentTo(material);
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == materialId)), Times.Once);
    }
    
    [Fact]
    public void BuscarEntidadePorId_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var materialId = 1;
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == materialId)))
            .Returns((Material)null);

        var resultado = () => _service.BuscarEntidadePorId(materialId);

        resultado.Should().Throw<NaoEncontradoException>();
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == materialId)), Times.Once);
    }
    
    [Fact]
    public void BuscarPorId_ComDadosValidos_DeveRealizarOperacao()
    {
        var materialId = 1;
        var material = _fixture.Build<Material>()
            .Create();
        var materialDto = _fixture.Create<MaterialDto>();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == materialId)))
            .Returns(material);
        _mapperMock.Setup(x => x.Map<MaterialDto>(It.Is<Material>(x => x == material)))
            .Returns(materialDto);

        var resultado = _service.BuscarPorId(materialId);

        resultado.Should().NotBeNull();
        resultado.Should().BeOfType<MaterialDto>();
        resultado.Should().BeEquivalentTo(materialDto);
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == materialId)), Times.Once);
        _mapperMock.Verify(x => x.Map<MaterialDto>(It.Is<Material>(x => x == material)), Times.Once);
    }
    
    [Fact]
    public void BuscarPorId_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var materialId = 1;
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == materialId)))
            .Returns((Material)null);

        var resultado = () => _service.BuscarPorId(materialId);

        resultado.Should().Throw<NaoEncontradoException>();
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == materialId)), Times.Once);
    }

    [Fact]
    public void Adicionar_ComDadosValidos_DeveRealizarOperacao()
    {
        var dto = _fixture.Build<MaterialDto>()
            .Create();
        _repositoryMock.Setup(x => x.Adicionar(It.IsAny<Material>()));

        var resultado = () => _service.Adicionar(dto);

        resultado.Should().NotThrow<MaterialInvalidoException>();
        _repositoryMock.Verify(x => x.Adicionar(It.IsAny<Material>()), Times.Once);
    }
    
    [Fact]
    public void Adicionar_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var dto = _fixture.Build<MaterialDto>()
            .Without(x => x.Nome)
            .Create();

        var resultado = () => _service.Adicionar(dto);

        resultado.Should().Throw<MaterialInvalidoException>();
    }
    
    [Fact]
    public void Editar_ComDadosValidos_DeveRealizarOperacao()
    {
        var materialId = 1;
        var dto = _fixture.Build<EditarMaterialDto>()
            .Create();
        var material = _fixture.Build<Material>()
            .Create();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == materialId))).Returns(material);
        _repositoryMock.Setup(x => x.Editar(It.IsAny<Material>()));

        var resultado = () => _service.Editar(materialId, dto);

        resultado.Should().NotThrow<NaoEncontradoException>();
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == materialId)), Times.Once);
        _repositoryMock.Verify(x => x.Editar(It.IsAny<Material>()), Times.Once);
    }
    
    [Fact]
    public void Editar_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var materialId = 1;
        var dto = _fixture.Build<EditarMaterialDto>()
            .Without(x => x.Nome)
            .Create();
        var material = _fixture.Build<Material>()
            .Create();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == materialId))).Returns(material);
        _repositoryMock.Setup(x => x.Editar(It.IsAny<Material>()));

        var resultado = () => _service.Editar(materialId, dto);
        
        resultado.Should().NotThrow<NaoEncontradoException>();
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == materialId)), Times.Once);
        _repositoryMock.Verify(x => x.Editar(It.IsAny<Material>()), Times.Once); 
    }
    
    [Fact]
    public void Excluir_ComDadosValidos_DeveRealizarOperacao()
    {
        var materialId = 1;
        var material = _fixture.Build<Material>()
            .Create();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == materialId))).Returns(material);
        _repositoryMock.Setup(x => x.Excluir(It.Is<Material>(x => x == material)));

        var resultado = () => _service.Excluir(materialId);

        resultado.Should().NotThrow<NaoEncontradoException>();
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == materialId)), Times.Once);
        _repositoryMock.Verify(x => x.Excluir(It.Is<Material>(x => x == material)), Times.Once);
    }
    
    [Fact]
    public void Excluir_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var materialId = 1;
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == materialId)))
            .Returns((Material)null);

        var resultado = () => _service.Excluir(materialId);

        resultado.Should().Throw<NaoEncontradoException>();
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == materialId)), Times.Once);
        _repositoryMock.Verify(x => x.Excluir(It.IsAny<Material>()), Times.Never);
    }

    [Fact]
    public void MovimentarEstoque_ComDadosValidos_DeveRealizarOperacao()
    {
        var materialId = 1;
        var quantidade = 1;
        var material = _fixture.Build<Material>()
            .Create();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == materialId))).Returns(material);
        _repositoryMock.Setup(x => x.Editar(It.IsAny<Material>()));

         _service.MovimentarEstoque(materialId, new EntradaSaidaMaterialDto() { Operacao = EntradaSaida.Entrada, Quantidade = quantidade });

        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == materialId)), Times.Once);
        _repositoryMock.Verify(x => x.Editar(It.IsAny<Material>()), Times.Once); 
    }
    
    [Theory]
    [InlineData(EntradaSaida.Entrada, -1)]
    [InlineData(EntradaSaida.Entrada, null)]
    [InlineData(EntradaSaida.Saida, -1)]
    [InlineData(EntradaSaida.Saida, null)]
    [InlineData(null, 1)]
    [InlineData(null, null)]
    public void MovimentarEstoque_ComDadosInvalidos_NaoDeveRealizarOperacao(EntradaSaida? operacao, int? quantidade)
    {
        var materialId = 1;
        var material = _fixture.Build<Material>()
            .Create();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == materialId))).Returns(material);

        var resultado = () => _service.MovimentarEstoque(materialId,
            new EntradaSaidaMaterialDto() { Operacao = operacao, Quantidade = quantidade });
        
        resultado.Should().Throw<OperacaoInvalidaException>();
        resultado.Should().NotThrow<NaoEncontradoException>();
        resultado.Should().NotThrow<EstoqueInvalidoException>();
    }
    
    [Fact]
    public void MovimentarEstoque_ComDadosInvalidosEMaterialInexistente_NaoDeveRealizarOperacao()
    {
        var materialId = 1;
        var quantidade = 1;
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == materialId))).Returns((Material)null);
        _repositoryMock.Setup(x => x.Editar(It.IsAny<Material>()));

        var resultado = () => _service.MovimentarEstoque(materialId,
            new EntradaSaidaMaterialDto() { Operacao = EntradaSaida.Entrada, Quantidade = quantidade });
        
        resultado.Should().Throw<NaoEncontradoException>();
        resultado.Should().NotThrow<OperacaoInvalidaException>();
        resultado.Should().NotThrow<EstoqueInvalidoException>();
    }
    
    [Fact]
    public void MovimentarEstoque_ComDadosInvalidosEMaterialSemEstoqueParaSaida_NaoDeveRealizarOperacao()
    {
        var materialId = 1;
        var quantidade = 1;
        var material = _fixture.Build<Material>()
            .Create();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == materialId))).Returns(material);
        _repositoryMock.Setup(x => x.Editar(It.IsAny<Material>()));

        var resultado = () => _service.MovimentarEstoque(materialId,
            new EntradaSaidaMaterialDto() { Operacao = EntradaSaida.Saida, Quantidade = quantidade });
        
        resultado.Should().Throw<OperacaoInvalidaException>();
        resultado.Should().NotThrow<NaoEncontradoException>();
        resultado.Should().NotThrow<EstoqueInvalidoException>();
    }
}