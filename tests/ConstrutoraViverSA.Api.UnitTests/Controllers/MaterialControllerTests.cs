using System;
using System.Linq;
using AutoFixture;
using AutoMapper;
using ConstrutoraViverSA.Api.Controllers;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Application.Interfaces;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace ConstrutoraViverSA.Api.UnitTests.Controllers;

public class MaterialControllerTests
{
    private readonly Mock<IMaterialService> _materialServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly MaterialController _controller;
    private readonly Fixture _fixture = new Fixture();

    public MaterialControllerTests()
    {
        _materialServiceMock = new Mock<IMaterialService>();
        _mapperMock = new Mock<IMapper>();
        _controller = new MaterialController(_materialServiceMock.Object, _mapperMock.Object);
    }

    [Fact]
    public void CadastrarMaterial_ComDadosValidos_DeveRealizarOperacao()
    {
        var request = _fixture.Create<MaterialRequest>();
        var dto = _fixture.Create<MaterialDto>();

        _mapperMock.Setup(x => x.Map<MaterialDto>(It.Is<MaterialRequest>(x => x == request))).Returns(dto);
        _materialServiceMock.Setup(x => x.Adicionar(It.Is<MaterialDto>(x => x == dto)));

        var resultado = _controller.CadastrarMaterial(request);

        resultado.Sucesso.Should().BeTrue();
        resultado.Dados.Should().BeNull();
        resultado.Mensagens.Should().BeNull();
        _materialServiceMock.Verify(x => x.Adicionar(It.Is<MaterialDto>(x => x == dto)), Times.Once);
        _mapperMock.Verify(x => x.Map<MaterialDto>(It.Is<MaterialRequest>(x => x == request)), Times.Once);
    }

    [Fact]
    public void CadastrarMaterial_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var request = _fixture.Build<MaterialRequest>()
            .Without(x => x.Nome)
            .Create();

        var resultado = () => _controller.CadastrarMaterial(request);

        resultado.Should().Throw<ErroValidacaoException>();
    }

    [Fact]
    public void BuscarMaterial_ComDadosValidos_DeveRealizarOperacao()
    {
        var materialId = 1;
        var material = _fixture.Create<MaterialDto>();
        _materialServiceMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == materialId))).Returns(material);

        var resultado = _controller.BuscarMaterial(materialId);

        resultado.Sucesso.Should().BeTrue();
        resultado.Mensagens.Should().BeNull();
        resultado.Dados.Should().NotBeNull();
        resultado.Dados.First().Should().BeOfType<MaterialDto>();
        resultado.Dados.First().Should().BeEquivalentTo(material);
        _materialServiceMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == materialId)), Times.Once);
    }

    [Fact]
    public void BuscarMaterial_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var materialId = 1;
        _materialServiceMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == materialId)))
            .Throws(new NaoEncontradoException("Material não encontrado"));

        var resultado = () => _controller.BuscarMaterial(materialId);

        resultado.Should().Throw<NaoEncontradoException>();
        _materialServiceMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == materialId)), Times.Once);
    }

    [Fact]
    public void BuscarMateriais_ComDadosValidos_DeveRealizarOperacao()
    {
        var listaMateriais = _fixture.CreateMany<MaterialDto>().ToList();
        _materialServiceMock.Setup(x => x.BuscarTodos())
            .Returns(listaMateriais);

        var resultado = _controller.BuscarMateriais();

        resultado.Sucesso.Should().BeTrue();
        resultado.Mensagens.Should().BeNull();
        resultado.Dados.Should().NotBeNull();
        resultado.Dados.Count.Should().Be(listaMateriais.Count);
        resultado.Dados.ForEach(x => x.Should().BeOfType<MaterialDto>());
        for (var i = 0; i < listaMateriais.Count; i++)
        {
            resultado.Dados[i].Should().BeEquivalentTo(listaMateriais[i]);
        }

        _materialServiceMock.Verify(x => x.BuscarTodos(), Times.Once);
    }

    [Fact]
    public void EditarMaterial_ComDadosValidos_DeveRealizarOperacao()
    {
        var materialId = 1;
        var request = _fixture.Build<EditarMaterialRequest>()
            .Create();
        var dto = _fixture.Build<EditarMaterialDto>()
            .Create();
        _mapperMock.Setup(x => x.Map<EditarMaterialDto>(It.Is<EditarMaterialRequest>(x => x == request))).Returns(dto);
        _materialServiceMock.Setup(x =>
            x.Editar(It.Is<long>(x => x == materialId), It.Is<EditarMaterialDto>(x => x == dto)));

        var resultado= _controller.EditarMaterial(request, materialId);

        resultado.Sucesso.Should().BeTrue();
        resultado.Dados.Should().BeNull();
        resultado.Mensagens.Should().BeNull();
        _materialServiceMock.Verify(
            X => X.Editar(It.Is<long>(x => x == materialId), It.Is<EditarMaterialDto>(x => x == dto)), Times.Once);
        _mapperMock.Verify(x => x.Map<EditarMaterialDto>(It.Is<EditarMaterialRequest>(x => x == request)), Times.Once);
    }

    [Fact]
    public void EditarMaterial_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var materialId = 1;
        var request = _fixture.Build<EditarMaterialRequest>()
            .Create();
        _materialServiceMock.Setup(x => x.Editar(It.Is<long>(x => x == materialId), It.IsAny<EditarMaterialDto>()))
            .Throws(new NaoEncontradoException("Material não encontrado"));

        var resultado = () => _controller.EditarMaterial(request, materialId);

        resultado.Should().Throw<NaoEncontradoException>();
        _materialServiceMock.Verify(x => x.Editar(It.Is<long>(x => x == materialId), It.IsAny<EditarMaterialDto>()),
            Times.Once);
    }

    [Fact]
    public void ExcluirMaterial_ComDadosValidos_DeveRealizarOperacao()
    {
        var materialId = 1;
        _materialServiceMock.Setup(x => x.Excluir(It.Is<long>(x => x == materialId)));

        var resultado = _controller.ExcluirMaterial(materialId);

        resultado.Sucesso.Should().BeTrue();
        resultado.Dados.Should().BeNull();
        resultado.Mensagens.Should().BeNull();
        _materialServiceMock.Verify(x => x.Excluir(It.Is<long>(x => x == materialId)), Times.Once);
    }

    [Fact]
    public void ExcluirMaterial_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var materialId = 1;
        _materialServiceMock.Setup(x => x.Excluir(It.Is<long>(x => x == materialId)))
            .Throws(new NaoEncontradoException("Material não encontrado"));

        var resultado = () => _controller.ExcluirMaterial(materialId);

        resultado.Should().Throw<NaoEncontradoException>();
        _materialServiceMock.Verify(x => x.Excluir(It.Is<long>(x => x == materialId)), Times.Once);
    }

    [Fact]
    public void MovimentarEstoque_ComDadosValidos_DeveRealizarOperacao()
    {
        var materialId = 1;
        var quantidade = 1;
        var request = _fixture.Build<EntradaSaidaMaterialRequest>()
            .With(x => x.Operacao, EntradaSaida.Entrada)
            .With(x => x.Quantidade, quantidade)
            .Create();
        var dto = _fixture.Build<EntradaSaidaMaterialDto>()
            .With(x => x.Operacao, EntradaSaida.Entrada)
            .With(x => x.Quantidade, quantidade)
            .Create();
        
        _mapperMock.Setup(x => x.Map<EntradaSaidaMaterialDto>(It.Is<EntradaSaidaMaterialRequest>(x => x == request))).Returns(dto);
        _materialServiceMock.Setup(x => x.MovimentarEstoque(It.Is<long>(x => x == materialId), It.Is<EntradaSaidaMaterialDto>(x => x == dto)));
        
        var resultado = _controller.MovimentarEstoque(request, materialId);
        
        resultado.Sucesso.Should().BeTrue();
        resultado.Dados.Should().BeNull();
        resultado.Mensagens.Should().BeNull();
        _materialServiceMock.Verify(X => X.MovimentarEstoque(It.Is<long>(x => x == materialId), It.Is<EntradaSaidaMaterialDto>(x => x == dto)), Times.Once);
        _mapperMock.Verify(x => x.Map<EntradaSaidaMaterialDto>(It.Is<EntradaSaidaMaterialRequest>(x => x == request)), Times.Once);
    }

    [Fact]
    public void MovimentarEstoque_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var materialId = 1;
        var quantidade = 1;
        var request = _fixture.Build<EntradaSaidaMaterialRequest>()
            .With(x => x.Operacao, EntradaSaida.Entrada)
            .With(x => x.Quantidade, quantidade)
            .Create();
        var dto = _fixture.Build<EntradaSaidaMaterialDto>()
            .With(x => x.Operacao, EntradaSaida.Entrada)
            .With(x => x.Quantidade, quantidade)
            .Create();
        _mapperMock.Setup(x => x.Map<EntradaSaidaMaterialDto>(It.Is<EntradaSaidaMaterialRequest>(x => x == request))).Returns(dto);
        _materialServiceMock.Setup(x => x.MovimentarEstoque(It.Is<long>(x => x == materialId), It.Is<EntradaSaidaMaterialDto>(x => x == dto)))
            .Throws(new NaoEncontradoException("Material não encontrado"));
        
        var resultado = () => _controller.MovimentarEstoque(request, materialId);
        
        resultado.Should().Throw<NaoEncontradoException>();
        _materialServiceMock.Verify(x => x.MovimentarEstoque(It.Is<long>(x => x == materialId), It.Is<EntradaSaidaMaterialDto>(x => x == dto)), Times.Once);
        _mapperMock.Verify(x => x.Map<EntradaSaidaMaterialDto>(It.Is<EntradaSaidaMaterialRequest>(x => x == request)), Times.Once);
    }
}