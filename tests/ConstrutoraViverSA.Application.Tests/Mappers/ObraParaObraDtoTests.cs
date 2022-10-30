using System;
using System.Collections.Generic;
using AutoFixture;
using AutoMapper;
using ConstrutoraViverSA.Application.Mappers;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Enums;
using FluentAssertions;
using Moq;
using Xunit;

namespace ConstrutoraViverSA.Application.Tests.Mappers;

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
        var nome = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var tipoObra = _fixture.Create<TipoObraEnum>();
        var descricao = _fixture.Create<string>();
        var valor = _fixture.Create<double>();
        var orcamento = new Orcamento("teste", "teste", TipoObraEnum.Ambas, DateTime.Today, DateTime.Today.AddDays(1),
            10.85);
        var prazoConclusao = DateTime.Today.AddDays(2);
        var obra = new Obra(nome, endereco, tipoObra, descricao, valor, prazoConclusao, orcamento, null, null);
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
        
    }
    
    [Fact]
    public void Mapear_ComDadosValidosESemFuncionariosEComMateriais_DeveRealizarOperacao()
    {
        
    }
    
    [Fact]
    public void Mapear_ComDadosValidosEComFuncionariosEMateriais_DeveRealizarOperacao()
    {
        
    }
}