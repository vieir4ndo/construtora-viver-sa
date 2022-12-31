using AutoFixture;
using AutoMapper;
using ConstrutoraViverSA.Application.Mappers;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Tests.Stubs;
using FluentAssertions;
using Moq;
using Xunit;

namespace ConstrutoraViverSA.Application.IntegrationTests.Mappers;

public class ObraParaObraDtoTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly ObraParaObraDto _mapper;
    private readonly Fixture _fixture = new ();

    public ObraParaObraDtoTests()
    {
        _mapperMock = new Mock<IMapper>();
        _mapper = new ObraParaObraDto(_mapperMock.Object);
    }
    
    [Fact]
    public void Mapear_ComDadosValidosESemFuncionariosEMateriais_DeveRealizarOperacao()
    {
        var obra = ObraStub.Valido(_fixture);
        var obraDto = _fixture.Build<ObraDto>()
            .Without(x => x.Funcionarios)
            .Without(x => x.Materiais)
            .Create();
        _mapperMock.Setup(x => x.Map<ObraDto>(It.Is<Obra>(x => x == obra))).Returns(obraDto);
        
        var resultado = _mapper.Mapear(obra);

        resultado.Should().NotBeNull();
        resultado.Should().BeOfType<ObraDto>();
        resultado.Funcionarios.Should().BeEmpty();
        resultado.Materiais.Should().BeEmpty();
        _mapperMock.Verify(x => x.Map<ObraDto>(It.Is<Obra>(x => x == obra)), Times.Once);
    }
    
    [Fact]
    public void Mapear_ComDadosValidosEComFuncionariosESemMateriais_DeveRealizarOperacao()
    {
        var funcionarios = new List<Funcionario>()
            { FuncionarioStub.Valido(_fixture), FuncionarioStub.Valido(_fixture), FuncionarioStub.Valido(_fixture) };
        var obra = ObraStub.ValidoComFuncionarios(_fixture, funcionarios);
        var obraDto = _fixture.Build<ObraDto>()
            .Without(x => x.Funcionarios)
            .Without(x => x.Materiais)
            .Create();
        _mapperMock.Setup(x => x.Map<ObraDto>(It.Is<Obra>(x => x == obra))).Returns(obraDto);
        
        var resultado = _mapper.Mapear(obra);

        resultado.Should().NotBeNull();
        resultado.Should().BeOfType<ObraDto>();
        resultado.Funcionarios.Should().NotBeEmpty();
        resultado.Funcionarios.Should().HaveCount(3);
        resultado.Materiais.Should().BeEmpty();
        _mapperMock.Verify(x => x.Map<ObraDto>(It.Is<Obra>(x => x == obra)), Times.Once);
    }
    
    [Fact]
    public void Mapear_ComDadosValidosESemFuncionariosEComMateriais_DeveRealizarOperacao()
    {
        var quantidade = 1;
        var materiais = new List<Material>()
            { MaterialStub.Valido(_fixture), MaterialStub.Valido(_fixture), MaterialStub.Valido(_fixture) };
        materiais.ForEach(x => x.MovimentarEstoque(EntradaSaida.Entrada, quantidade));
        var dicionarioMateriais = new Dictionary<Material, int>();
        materiais.ForEach(x => dicionarioMateriais.Add(x, quantidade));
        var obra = ObraStub.ValidoComMateriais(_fixture, dicionarioMateriais);
        var obraDto = _fixture.Build<ObraDto>()
            .Without(x => x.Funcionarios)
            .Without(x => x.Materiais)
            .Create();
        _mapperMock.Setup(x => x.Map<ObraDto>(It.Is<Obra>(x => x == obra))).Returns(obraDto);
        
        var resultado = _mapper.Mapear(obra);

        resultado.Should().NotBeNull();
        resultado.Should().BeOfType<ObraDto>();
        resultado.Materiais.Should().NotBeEmpty();
        resultado.Materiais.Should().HaveCount(1);
        resultado.Funcionarios.Should().BeEmpty();
        _mapperMock.Verify(x => x.Map<ObraDto>(It.Is<Obra>(x => x == obra)), Times.Once);
    }
    
    [Fact]
    public void Mapear_ComDadosValidosEComFuncionariosEMateriais_DeveRealizarOperacao()
    { 
        var quantidade = 1;
        var materiais = new List<Material>()
            { MaterialStub.Valido(_fixture), MaterialStub.Valido(_fixture), MaterialStub.Valido(_fixture) };
        materiais.ForEach(x => x.MovimentarEstoque(EntradaSaida.Entrada, quantidade));
        var dicionarioMateriais = new Dictionary<Material, int>();
        materiais.ForEach(x => dicionarioMateriais.Add(x, quantidade));
        var funcionarios = new List<Funcionario>()
            { FuncionarioStub.Valido(_fixture), FuncionarioStub.Valido(_fixture), FuncionarioStub.Valido(_fixture) };
        var obra = ObraStub.ValidoComFuncionariosEMateriais(_fixture, funcionarios, dicionarioMateriais);
        var obraDto = _fixture.Build<ObraDto>()
            .Without(x => x.Funcionarios)
            .Without(x => x.Materiais)
            .Create();
        _mapperMock.Setup(x => x.Map<ObraDto>(It.Is<Obra>(x => x == obra))).Returns(obraDto);
        
        var resultado = _mapper.Mapear(obra);

        resultado.Should().NotBeNull();
        resultado.Should().BeOfType<ObraDto>();
        resultado.Materiais.Should().NotBeEmpty();
        resultado.Materiais.Should().HaveCount(1);
        resultado.Funcionarios.Should().NotBeEmpty();
        resultado.Funcionarios.Should().HaveCount(3);
        _mapperMock.Verify(x => x.Map<ObraDto>(It.Is<Obra>(x => x == obra)), Times.Once);
    }
}