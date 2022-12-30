using AutoFixture;
using AutoMapper;
using ConstrutoraViverSA.Application.Interfaces;
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
        _service = new ObraService(_repositoryMock.Object, _orcamentoServiceMock.Object, _funcionarioServiceMock.Object, _materialServiceMock.Object, _obraParaObraDtoServiceMock.Object);
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
            .Throws<NaoEncontradoException>();

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
            .Throws<NaoEncontradoException>();

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
        var orcamentoId = 1;
        var dto = _fixture.Build<ObraDto>()
            .With(x => x.OrcamentoId, orcamentoId)
            .Without(x => x.Funcionarios)
            .Without(x => x.Materiais)
            .Create();
        _orcamentoServiceMock.Setup(x => x.BuscarEntidadePorId(It.Is<long>(x => x == orcamentoId))).Throws<NaoEncontradoException>();

        var resultado = () => _service.Adicionar(dto);

        resultado.Should().Throw<NaoEncontradoException>();
    }
    
    [Fact]
    public void Adicionar_ComDadosInvalidosEFuncionarioInexistente_NaoDeveRealizarOperacao()
    {
        var orcamentoId = 1;
        var funcionarios = new List<long>() { 1 };
        var dto = _fixture.Build<ObraDto>()
            .With(x => x.OrcamentoId, orcamentoId)
            .With(x => x.Funcionarios, funcionarios)
            .Without(x => x.Materiais)
            .Create();
        var orcamento = _fixture.Build<Orcamento>()
            .With(x => x.Id, orcamentoId)
            .Create();
        _orcamentoServiceMock.Setup(x => x.BuscarEntidadePorId(It.Is<long>(x => x == orcamentoId))).Returns(orcamento);

        var resultado = () => _service.Adicionar(dto);

        resultado.Should().NotThrow<ObraInvalidaException>();
    }
    
    [Fact]
    public void Adicionar_ComDadosInvalidosEMaterialInexistente_NaoDeveRealizarOperacao()
    {
        var orcamentoId = 1;
        var materialId = 1;
        var quantidadeMaterial = 1;
        var materiais = new Dictionary<long, int>() { {materialId, quantidadeMaterial}};
        var dto = _fixture.Build<ObraDto>()
            .With(x => x.OrcamentoId, orcamentoId)
            .Without(x => x.Funcionarios)
            .With(x => x.Materiais, materiais)
            .Create();
        var orcamento = _fixture.Build<Orcamento>()
            .With(x => x.Id, orcamentoId)
            .Create();
        _orcamentoServiceMock.Setup(x => x.BuscarEntidadePorId(It.Is<long>(x => x == orcamentoId))).Returns(orcamento);
        _materialServiceMock.Setup(x => x.BuscarEntidadePorId(It.Is<long>(x => x == materialId)))
            .Throws(new NaoEncontradoException("Material não encontrado"));
        
        var resultado = () => _service.Adicionar(dto);

        resultado.Should().Throw<NaoEncontradoException>();
    }

    [Fact]
    public void Editar_ComDadosValidos_DeveRealizarOperacao()
    {
        var obraId = 1;
        var obraDto = _fixture.Build<ObraDto>()
            .Without(x => x.Funcionarios)
            .Without(x => x.Materiais)
            .Without(x => x.OrcamentoId)
            .Without(x => x.PrazoConclusao)
            .Create();
        var obra = _fixture.Create<Obra>();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == obraId)))
            .Returns(obra);
        _repositoryMock.Setup(x => x.Editar(It.Is<Obra>(x => x == obra)));
       
        _service.Editar(obraId, obraDto);
        
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == obraId)), Times.Once);
        _repositoryMock.Verify(x => x.Editar(It.Is<Obra>(x => x == obra)), Times.Once);
    }
    
    [Fact]
    public void Editar_ComDadosValidosEOrcamento_DeveRealizarOperacao()
    {
        var obraId = 1;
        var orcamentoId = 1;
        var obraDto = _fixture.Build<ObraDto>()
            .Without(x => x.Funcionarios)
            .Without(x => x.Materiais)
            .With(x => x.OrcamentoId, orcamentoId)
            .Without(x => x.PrazoConclusao)
            .Create();
        var obra = _fixture.Create<Obra>();
        var orcamento = _fixture.Create<Orcamento>();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == obraId)))
            .Returns(obra);
        _repositoryMock.Setup(x => x.Editar(It.Is<Obra>(x => x == obra)));
        _orcamentoServiceMock.Setup(x => x.BuscarEntidadePorId(It.Is<long>(x => x == orcamentoId))).Returns(orcamento);
        
        _service.Editar(obraId, obraDto);
        
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == obraId)), Times.Once);
        _repositoryMock.Verify(x => x.Editar(It.Is<Obra>(x => x == obra)), Times.Once);
        _orcamentoServiceMock.Verify(x => x.BuscarEntidadePorId(It.Is<long>(x => x == orcamentoId)), Times.Once);
    }
    
    [Fact]
    public void Editar_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var obraId = 1;
        var obraDto = _fixture.Build<ObraDto>()
            .Without(x => x.Funcionarios)
            .Without(x => x.Materiais)
            .Create();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == obraId)))
            .Throws(new NaoEncontradoException("Obra não encontrada"));
        
        var resultado = () => _service.Editar(obraId, obraDto);

        resultado.Should().Throw<NaoEncontradoException>();
    }
    
    [Fact]
    public void Editar_ComDadosInvalidosEOrcamentoInexistente_NaoDeveRealizarOperacao()
    {
        var obraId = 1;
        var orcamentoId = 1;
        var obraDto = _fixture.Build<ObraDto>()
            .Without(x => x.Funcionarios)
            .Without(x => x.Materiais)
            .With(x => x.OrcamentoId, orcamentoId)
            .Without(x => x.PrazoConclusao)
            .Create();
        var obra = _fixture.Create<Obra>();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == obraId)))
            .Returns(obra);
        _repositoryMock.Setup(x => x.Editar(It.Is<Obra>(x => x == obra)));
        _orcamentoServiceMock.Setup(x => x.BuscarEntidadePorId(It.Is<long>(x => x == orcamentoId))).Throws(new NaoEncontradoException("Orcamento não encontrado"));
        
        var resultado = () =>_service.Editar(obraId, obraDto);

        resultado.Should().Throw<NaoEncontradoException>();
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
            .Throws<NaoEncontradoException>();

        var resultado = () => _service.Excluir(obraId);

        resultado.Should().Throw<NaoEncontradoException>();
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == obraId)), Times.Once);
    }

    [Fact]
    public void GerenciarMaterial_ComDadosValidosAlocandoMaterial_DeveRealizarOperacao()
    {
        var obraId = 1;
        var materialId = 1;
        var quantidade = 1;
        var dto = _fixture.Build<EntradaSaidaMaterialDto>()
            .With(x => x.Operacao, EntradaSaida.Entrada)
            .With(x => x.Quantidade, quantidade)
            .Create();
        var material = _fixture.Create<Material>();
        material.MovimentarEstoque(EntradaSaida.Entrada, quantidade);
        _materialServiceMock.Setup(x => x.BuscarEntidadePorId(It.Is<long>(x => x == materialId))).Returns(material);
        var obra = _fixture.Build<Obra>()
            .Create();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == obraId))).Returns(obra);
        _repositoryMock.Setup(x => x.Editar(It.Is<Obra>(x => x == obra)));
        
        _service.GerenciarMaterial(dto, obraId, materialId);

        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == obraId)), Times.Once);
        _materialServiceMock.Verify(x => x.BuscarEntidadePorId(It.Is<long>(x => x == materialId)), Times.Once);
        _repositoryMock.Verify(x => x.Editar(It.Is<Obra>(x => x == obra)), Times.Once);
    }
    
    [Fact]
    public void GerenciarMaterial_ComDadosValidosDesalocando_DeveRealizarOperacao()
    {
        var obraId = 1;
        var materialId = 1;
        var quantidade = 1;
        var dto = _fixture.Build<EntradaSaidaMaterialDto>()
            .With(x => x.Operacao, EntradaSaida.Saida)
            .With(x => x.Quantidade, quantidade)
            .Create();
        var material = _fixture.Build<Material>().With(x => x.Id, materialId).Create();
        material.MovimentarEstoque(EntradaSaida.Entrada, quantidade);
        _materialServiceMock.Setup(x => x.BuscarEntidadePorId(It.Is<long>(x => x == materialId))).Returns(material);
        var obra = _fixture.Build<Obra>()
            .Create();
        obra.AlocarMaterial(material, quantidade);
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == obraId))).Returns(obra);
        _repositoryMock.Setup(x => x.Editar(It.Is<Obra>(x => x == obra)));
        
        _service.GerenciarMaterial(dto, obraId, materialId);

        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == obraId)), Times.Once);
        _materialServiceMock.Verify(x => x.BuscarEntidadePorId(It.Is<long>(x => x == materialId)), Times.Once);
        _repositoryMock.Verify(x => x.Editar(It.Is<Obra>(x => x == obra)), Times.Once);
    }
    
    [Fact]
    public void GerenciarMaterial_ComDadosInvalidos_NaoDeveRealizarOperacao()
    {
        var obraId = 1;
        var materialId = 1;
        var quantidade = 1;
        var dto = _fixture.Build<EntradaSaidaMaterialDto>()
            .With(x => x.Operacao, EntradaSaida.Saida)
            .Without(x => x.Quantidade)
            .Create();
        var material = _fixture.Create<Material>();
        material.MovimentarEstoque(EntradaSaida.Entrada, quantidade);
        _materialServiceMock.Setup(x => x.BuscarEntidadePorId(It.Is<long>(x => x == materialId))).Returns(material);
        var obra = _fixture.Build<Obra>()
            .Create();
        obra.AlocarMaterial(material, quantidade);
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == obraId))).Returns(obra);
        _repositoryMock.Setup(x => x.Editar(It.Is<Obra>(x => x == obra)));

        var resultado = () => _service.GerenciarMaterial(dto, obraId, materialId);

        resultado.Should().Throw<OperacaoInvalidaException>();
    }
    
    [Fact]
    public void GerenciarMaterial_ComDadosInvalidosEMaterialInexistente_NaoDeveRealizarOperacao()
    {
        var obraId = 1;
        var materialId = 1;
        var quantidade = 1;
        var dto = _fixture.Build<EntradaSaidaMaterialDto>()
            .With(x => x.Operacao, EntradaSaida.Saida)
            .With(x => x.Quantidade, quantidade)
            .Create();
        _materialServiceMock.Setup(x => x.BuscarEntidadePorId(It.Is<long>(x => x == materialId))).Throws(new NaoEncontradoException("Material não encontrado"));
        var obra = _fixture.Build<Obra>()
            .Create();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == obraId))).Returns(obra);
        _repositoryMock.Setup(x => x.Editar(It.Is<Obra>(x => x == obra)));

        var resultado = () => _service.GerenciarMaterial(dto, obraId, materialId);

        resultado.Should().Throw<NaoEncontradoException>();
    }
    
    [Fact]
    public void AlocarFuncionario_ComDadosValidos_DeveRealizarOperacao()
    {
        var obraId = 1;
        var funcionarioId = 1;
        var obra = _fixture.Build<Obra>().Create();
        var funcionario = _fixture.Build<Funcionario>().Create();
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == obraId))).Returns(obra);
        _funcionarioServiceMock.Setup(x => x.BuscarEntidadePorId(It.Is<long>(x => x == funcionarioId))).Returns(funcionario);

        var resultado = () => _service.AlocarFuncionario(obraId, funcionarioId);

        resultado.Should().NotThrow<NaoEncontradoException>();
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == obraId)), Times.Once);
        _funcionarioServiceMock.Verify(x => x.BuscarEntidadePorId(It.Is<long>(x => x == funcionarioId)), Times.Once);
    }
    
    [Fact]
    public void AlocarFuncionario_ComDadosInvalidosEFuncionarioInexistente_NaoDeveRealizarOperacao()
    {
        var obraId = 1;
        var funcionarioId = 1;
        _funcionarioServiceMock.Setup(x => x.BuscarEntidadePorId(It.Is<long>(x => x == funcionarioId))).Throws(new NaoEncontradoException("Funcionario não encontrado"));

        var resultado = () => _service.AlocarFuncionario(obraId, funcionarioId);

        resultado.Should().Throw<NaoEncontradoException>();
        _funcionarioServiceMock.Verify(x => x.BuscarEntidadePorId(It.Is<long>(x => x == funcionarioId)), Times.Once);
    }
    
    [Fact]
    public void DesalocarFuncionario_ComDadosValidos_DeveRealizarOperacao()
    {
        var obraId = 1;
        var funcionarioId = 1;
        var funcionario = _fixture.Build<Funcionario>().Create();
        var obra = _fixture.Build<Obra>().Create();
        obra.AlocarFuncionario(funcionario);
        _repositoryMock.Setup(x => x.BuscarPorId(It.Is<long>(x => x == obraId))).Returns(obra);
        _funcionarioServiceMock.Setup(x => x.BuscarEntidadePorId(It.Is<long>(x => x == funcionarioId))).Returns(funcionario);

        var resultado = () => _service.DesalocarFuncionario(obraId, funcionarioId);

        resultado.Should().NotThrow<NaoEncontradoException>();
        _repositoryMock.Verify(x => x.BuscarPorId(It.Is<long>(x => x == obraId)), Times.Once);
        _funcionarioServiceMock.Verify(x => x.BuscarEntidadePorId(It.Is<long>(x => x == funcionarioId)), Times.Once);
    }
    
    [Fact]
    public void DesalocarFuncionario_ComDadosInvalidosEFuncionarioInexistente_NaoDeveRealizarOperacao()
    {
        var obraId = 1;
        var funcionarioId = 1;
        _funcionarioServiceMock.Setup(x => x.BuscarEntidadePorId(It.Is<long>(x => x == funcionarioId))).Throws(new NaoEncontradoException("Funcionário não encontrado"));

        var resultado = () => _service.DesalocarFuncionario(obraId, funcionarioId);

        resultado.Should().Throw<NaoEncontradoException>();
        _funcionarioServiceMock.Verify(x => x.BuscarEntidadePorId(It.Is<long>(x => x == funcionarioId)), Times.Once);
    }
}