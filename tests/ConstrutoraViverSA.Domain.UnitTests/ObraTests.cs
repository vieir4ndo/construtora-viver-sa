using AutoFixture;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;
using ConstrutoraViverSA.Domain.Models;
using ConstrutoraViverSA.Domain.Tests.Stubs;
using FluentAssertions;
using Xunit;

namespace ConstrutoraViverSA.Domain.Tests;

public class ObraTests
{
    private readonly Fixture _fixture = new Fixture();

    [Fact]
    public void Construtor_ComDadosValidos_DeveConstruirCorretamente()
    {
        var nome = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var tipoObra = _fixture.Create<TipoObra>();
        var descricao = _fixture.Create<string>();
        var valor = _fixture.Create<double>();
        var orcamento = new Orcamento("teste", "teste", TipoObra.Ambas, DateTime.Today, DateTime.Today.AddDays(1),
            10.85);
        var prazoConclusao = DateTime.Today.AddDays(2);
        
        var model = new ObraModel()
        {
            Nome = nome,
            Endereco = endereco, 
            TipoObra = tipoObra,
            Descricao = descricao,
            Valor = valor,
            PrazoConclusao = prazoConclusao,
            Orcamento = orcamento,
            Funcionarios = null,
            Materiais = null
        };

        var obra = new Obra(model);

        obra.Should().NotBeNull();
        obra.Nome.Should().Be(nome);
        obra.Endereco.Should().Be(endereco);
        obra.TipoObra.Should().Be(tipoObra);
        obra.Descricao.Should().Be(descricao);
        obra.Valor.Should().Be(valor);
        obra.Orcamento.Should().BeEquivalentTo(orcamento);
        obra.PrazoConclusao.Should().Be(prazoConclusao);
    }
    
    [Fact]
    public void Construtor_ComDadosValidosEFuncionarios_DeveConstruirCorretamente()
    {
        var nome = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var tipoObra = _fixture.Create<TipoObra>();
        var descricao = _fixture.Create<string>();
        var valor = _fixture.Create<double>();
        var orcamento = new Orcamento("teste", "teste", TipoObra.Ambas, DateTime.Today, DateTime.Today.AddDays(1),
            10.85);
        var prazoConclusao = DateTime.Today.AddDays(2);
        var funcionarios = new List<Funcionario>() { FuncionarioStub.Valido(_fixture) };
        
        var model = new ObraModel()
        {
            Nome = nome,
            Endereco = endereco, 
            TipoObra = tipoObra,
            Descricao = descricao,
            Valor = valor,
            PrazoConclusao = prazoConclusao,
            Orcamento = orcamento,
            Funcionarios = funcionarios,
            Materiais = null
        };

        var obra = new Obra(model);

        obra.Should().NotBeNull();
        obra.Nome.Should().Be(nome);
        obra.Endereco.Should().Be(endereco);
        obra.TipoObra.Should().Be(tipoObra);
        obra.Descricao.Should().Be(descricao);
        obra.Valor.Should().Be(valor);
        obra.Orcamento.Should().BeEquivalentTo(orcamento);
        obra.PrazoConclusao.Should().Be(prazoConclusao);
        obra.Funcionarios!.Count.Should().Be(funcionarios.Count);
    }
    
    [Fact]
    public void Construtor_ComDadosValidosEMateriais_DeveConstruirCorretamente()
    {
        var nome = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var tipoObra = _fixture.Create<TipoObra>();
        var descricao = _fixture.Create<string>();
        var valor = _fixture.Create<double>();
        var orcamento = new Orcamento("teste", "teste", TipoObra.Ambas, DateTime.Today, DateTime.Today.AddDays(1),
            10.85);
        var prazoConclusao = DateTime.Today.AddDays(2);
        var materiais = new List<Material>()
        {
            new Material("TESTE", "teste", TipoMaterial.Cimento, 10.80, 1),
            new Material("TESTE", "teste", TipoMaterial.Cimento, 10.80, 1),
            new Material("TESTE", "teste", TipoMaterial.Cimento, 10.80, 1),
            new Material("TESTE", "teste", TipoMaterial.Cimento, 10.80, 1),
            new Material("TESTE", "teste", TipoMaterial.Cimento, 10.80, 1),
        };
        var dicionarioMateriais = new Dictionary<Material, int>();
        materiais.ForEach(x => dicionarioMateriais.Add(x, 1));

        var model = new ObraModel()
        {
            Nome = nome,
            Endereco = endereco, 
            TipoObra = tipoObra,
            Descricao = descricao,
            Valor = valor,
            PrazoConclusao = prazoConclusao,
            Orcamento = orcamento,
            Funcionarios = null,
            Materiais = dicionarioMateriais
        };
        
        var obra = new Obra(model);

        obra.Should().NotBeNull();
        obra.Nome.Should().Be(nome);
        obra.Endereco.Should().Be(endereco);
        obra.TipoObra.Should().Be(tipoObra);
        obra.Descricao.Should().Be(descricao);
        obra.Valor.Should().Be(valor);
        obra.Orcamento.Should().BeEquivalentTo(orcamento);
        obra.PrazoConclusao.Should().Be(prazoConclusao);
        obra.ObraMateriais!.Count.Should().Be(materiais.Count);
    }
    
    [Fact]
    public void Construtor_ComNomeInvalido_DeveLancarExcecao()
    {
        var endereco = _fixture.Create<string>();
        var tipoObra = _fixture.Create<TipoObra>();
        var descricao = _fixture.Create<string>();
        var valor = _fixture.Create<double>();
        var orcamento = new Orcamento("teste", "teste", TipoObra.Ambas, DateTime.Today, DateTime.Today.AddDays(1),
            10.85);
        var prazoConclusao = DateTime.Today.AddDays(2);
        Obra? obra = null;
        
        var model = new ObraModel()
        {
            Nome = null,
            Endereco = endereco, 
            TipoObra = tipoObra,
            Descricao = descricao,
            Valor = valor,
            PrazoConclusao = prazoConclusao,
            Orcamento = orcamento,
            Funcionarios = null,
            Materiais = null
        };

        var result = () =>
        {
            obra = new Obra(model);
        };

        obra.Should().BeNull();
        result.Should().Throw<ObraInvalidaException>();
    }
    
    [Fact]
    public void Construtor_ComEnderecoInvalido_DeveLancarExcecao()
    {
        var nome = _fixture.Create<string>();
        var tipoObra = _fixture.Create<TipoObra>();
        var descricao = _fixture.Create<string>();
        var valor = _fixture.Create<double>();
        var orcamento = new Orcamento("teste", "teste", TipoObra.Ambas, DateTime.Today, DateTime.Today.AddDays(1),
            10.85);
        var prazoConclusao = DateTime.Today.AddDays(2);
        Obra? obra = null;
        
        var model = new ObraModel()
        {
            Nome = nome,
            Endereco = null, 
            TipoObra = tipoObra,
            Descricao = descricao,
            Valor = valor,
            PrazoConclusao = prazoConclusao,
            Orcamento = orcamento,
            Funcionarios = null,
            Materiais = null
        };

        var result = () =>
        {
            obra = new Obra(model);
        };

        obra.Should().BeNull();
        result.Should().Throw<ObraInvalidaException>();
    }
    
    [Fact]
    public void Construtor_ComTipoObraInvalido_DeveLancarExcecao()
    {
        var nome = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var descricao = _fixture.Create<string>();
        var valor = _fixture.Create<double>();
        var orcamento = new Orcamento("teste", "teste", TipoObra.Ambas, DateTime.Today, DateTime.Today.AddDays(1),
            10.85);
        var prazoConclusao = DateTime.Today.AddDays(2);
        Obra? obra = null;
        
        var model = new ObraModel()
        {
            Nome = nome,
            Endereco = endereco, 
            TipoObra = null,
            Descricao = descricao,
            Valor = valor,
            PrazoConclusao = prazoConclusao,
            Orcamento = orcamento,
            Funcionarios = null,
            Materiais = null
        };
        
        var result = () =>
        {
            obra = new Obra(model);
        };

        obra.Should().BeNull();
        result.Should().Throw<ObraInvalidaException>();
    }
    
    [Fact]
    public void Construtor_ComDescricaoInvalido_DeveLancarExcecao()
    {
        var nome = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var tipoObra = _fixture.Create<TipoObra>();
        var valor = _fixture.Create<double>();
        var orcamento = new Orcamento("teste", "teste", TipoObra.Ambas, DateTime.Today, DateTime.Today.AddDays(1),
            10.85);
        var prazoConclusao = DateTime.Today.AddDays(2);
        Obra? obra = null;
        
        var model = new ObraModel()
        {
            Nome = nome,
            Endereco = endereco, 
            TipoObra = tipoObra,
            Descricao = null,
            Valor = valor,
            PrazoConclusao = prazoConclusao,
            Orcamento = orcamento,
            Funcionarios = null,
            Materiais = null
        };
        
        var result = () =>
        {
            obra = new Obra(model);
        };

        obra.Should().BeNull();
        result.Should().Throw<ObraInvalidaException>();
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData(0)]
    public void Construtor_ComValorInvalido_DeveLancarExcecao(double? valor)
    {
        var nome = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var tipoObra = _fixture.Create<TipoObra>();
        var descricao = _fixture.Create<string>();
        var orcamento = new Orcamento("teste", "teste", TipoObra.Ambas, DateTime.Today, DateTime.Today.AddDays(1),
            10.85);
        var prazoConclusao = DateTime.Today.AddDays(2);
        Obra? obra = null;
        
        var model = new ObraModel()
        {
            Nome = nome,
            Endereco = endereco, 
            TipoObra = tipoObra,
            Descricao = descricao,
            Valor = valor,
            PrazoConclusao = prazoConclusao,
            Orcamento = orcamento,
            Funcionarios = null,
            Materiais = null
        };
        
        var result = () =>
        {
            obra = new Obra(model);
        };

        obra.Should().BeNull();
        result.Should().Throw<ObraInvalidaException>();
    }
    
    [Fact]
    public void Construtor_ComOrcamentoInvalido_DeveLancarExcecao()
    {
        var nome = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var tipoObra = _fixture.Create<TipoObra>();
        var descricao = _fixture.Create<string>();
        var valor = _fixture.Create<double>();
        var prazoConclusao = DateTime.Today.AddDays(2);
        Obra? obra = null;
        
        var model = new ObraModel()
        {
            Nome = nome,
            Endereco = endereco, 
            TipoObra = tipoObra,
            Descricao = descricao,
            Valor = valor,
            PrazoConclusao = prazoConclusao,
            Orcamento = null,
            Funcionarios = null,
            Materiais = null
        };
        
        var result = () =>
        {
            obra = new Obra(model);
        };
        
        obra.Should().BeNull();
        result.Should().Throw<ObraInvalidaException>();
    }
    
    [Fact]
    public void Construtor_ComPrazoConclusaoNull_DeveLancarExcecao()
    {
        var nome = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var tipoObra = _fixture.Create<TipoObra>();
        var descricao = _fixture.Create<string>();
        var valor = _fixture.Create<double>();
        var orcamento = new Orcamento("teste", "teste", TipoObra.Ambas, DateTime.Today, DateTime.Today.AddDays(1),
            10.85);
        Obra? obra = null;
        
        var model = new ObraModel()
        {
            Nome = nome,
            Endereco = endereco, 
            TipoObra = tipoObra,
            Descricao = descricao,
            Valor = valor,
            PrazoConclusao = null,
            Orcamento = orcamento,
            Funcionarios = null,
            Materiais = null
        };
        
        var result = () =>
        {
            obra = new Obra(model);
        };

        obra.Should().BeNull();
        result.Should().Throw<ObraInvalidaException>();
    }
    
    [Fact]
    public void Construtor_ComPrazoConclusaoInvalido_DeveLancarExcecao()
    {
        var nome = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var tipoObra = _fixture.Create<TipoObra>();
        var descricao = _fixture.Create<string>();
        var valor = _fixture.Create<double>();
        var orcamento = new Orcamento("teste", "teste", TipoObra.Ambas, DateTime.Today, DateTime.Today.AddDays(1),
            10.85);
        var prazoConclusao = DateTime.Today.AddDays(-1);
        Obra? obra = null;
        
        var model = new ObraModel()
        {
            Nome = nome,
            Endereco = endereco, 
            TipoObra = tipoObra,
            Descricao = descricao,
            Valor = valor,
            PrazoConclusao = prazoConclusao,
            Orcamento = orcamento,
            Funcionarios = null,
            Materiais = null
        };
        
        var result = () =>
        {
            obra = new Obra(model);
        };

        obra.Should().BeNull();
        result.Should().Throw<ObraInvalidaException>();
    }
    
    [Fact]
    public void SetNome_ComDadosValidos_DeveRealizarAlteracao()
    {
        var obra = ObraStub.Valido(_fixture);
        var nomeNovo = _fixture.Create<string>();
        
        obra.SetNome(nomeNovo);

        obra.Nome.Should().Be(nomeNovo);
    }
    
    [Fact]
    public void SetNome_ComDadosInvalidos_NaoDeveRealizarAlteracao()
    {
        var obra = ObraStub.Valido(_fixture);
        var nomeAntigo = obra.Nome;
        
        obra.SetNome(null);

        obra.Nome.Should().Be(nomeAntigo);
    }
    
    [Fact]
    public void SetEndereco_ComDadosValidos_DeveRealizarAlteracao()
    {
        var obra = ObraStub.Valido(_fixture);
        var enderecoNovo = _fixture.Create<string>();
        
        obra.SetEndereco(enderecoNovo);

        obra.Endereco.Should().Be(enderecoNovo);
    }
    
    [Fact]
    public void SetEndereco_ComDadosInvalidos_NaoDeveRealizarAlteracao()
    {
        var obra = ObraStub.Valido(_fixture);
        var enderecoAntigo = obra.Endereco;
        
        obra.SetEndereco(null);

        obra.Endereco.Should().Be(enderecoAntigo);
    }
    
    [Fact]
    public void SetDescricao_ComDadosValidos_DeveRealizarAlteracao()
    {
        var obra = ObraStub.Valido(_fixture);
        var enderecoNovo = _fixture.Create<string>();
        
        obra.SetDescricao(enderecoNovo);

        obra.Descricao.Should().Be(enderecoNovo);
    }
    
    [Fact]
    public void SetDescricao_ComDadosInvalidos_NaoDeveRealizarAlteracao()
    {
        var obra = ObraStub.Valido(_fixture);
        var enderecoAntigo = obra.Descricao;
        
        obra.SetDescricao(null);

        obra.Descricao.Should().Be(enderecoAntigo);
    }
    
    [Fact]
    public void SetTipoObra_ComDadosValidos_DeveRealizarAlteracao()
    {
        var obra = ObraStub.Valido(_fixture);
        var tipoObraNovo = _fixture.Create<TipoObra>();
        
        obra.SetTipoObra(tipoObraNovo);

        obra.TipoObra.Should().Be(tipoObraNovo);
    }
    
    [Fact]
    public void SetTipoObra_ComDadosInvalidos_NaoDeveRealizarAlteracao()
    {
        var obra = ObraStub.Valido(_fixture);
        var tipoObraAntigo = obra.TipoObra;
        
        obra.SetTipoObra(null);

        obra.TipoObra.Should().Be(tipoObraAntigo);
    }
    
    [Fact]
    public void SetValor_ComDadosValidos_DeveRealizarAlteracao()
    {
        var obra = ObraStub.Valido(_fixture);
        var valorNovo = _fixture.Create<double>();
        
        obra.SetValor(valorNovo);

        obra.Valor.Should().Be(valorNovo);
    }
    
    [Fact]
    public void SetValor_ComDadosInvalidos_NaoDeveRealizarAlteracao()
    {
        var obra = ObraStub.Valido(_fixture);
        var valorAntigo = obra.Valor;
        
        obra.SetValor(null);

        obra.Valor.Should().Be(valorAntigo);
    }
    
    [Fact]
    public void SetOrcamento_ComDadosValidos_DeveRealizarAlteracao()
    {
        var obra = ObraStub.Valido(_fixture);
        var orcamentoNovo = OrcamentoStub.Valido(_fixture);
        
        obra.SetOrcamento(orcamentoNovo);

        obra.Orcamento.Should().Be(orcamentoNovo);
    }
    
    [Fact]
    public void SetOrcamento_ComDadosInvalidos_NaoDeveRealizarAlteracao()
    {
        var obra = ObraStub.Valido(_fixture);
        var orcamentoAntigo = obra.Orcamento;
        
        obra.SetOrcamento(null);

        obra.Orcamento.Should().BeEquivalentTo(orcamentoAntigo);
    }
    
    [Fact]
    public void SetPrazoConclusao_ComDadosValidos_DeveRealizarAlteracao()
    {
        var prazoConclusao = DateTime.Today.AddDays(2);
       
        var obra = ObraStub.ValidoComPrazoDeConclusao(_fixture, prazoConclusao);

        var prazoConclusaoNovo = DateTime.Today.AddDays(1);
        
        obra.SetPrazoConclusao(prazoConclusaoNovo);

        obra.PrazoConclusao.Should().Be(prazoConclusaoNovo);
    }
    
    [Fact]
    public void SetPrazoConclusao_ComDadoNull_NaoDeveRealizarAlteracao()
    {
        var obra = ObraStub.Valido(_fixture);
        var prazoConclusaoAntigo = obra.PrazoConclusao;
        
        obra.SetPrazoConclusao(null);

        obra.PrazoConclusao.Should().Be(prazoConclusaoAntigo);
    }
    
    [Fact]
    public void SetPrazoConclusao_ComDadosInvalidos_NaoDeveRealizarAlteracao()
    {
        var prazoConclusao = DateTime.Today.AddDays(2);
        var obra = ObraStub.ValidoComPrazoDeConclusao(_fixture, prazoConclusao);

        var prazoConclusaoNovo = DateTime.Today.AddDays(-1);
        
        obra.SetPrazoConclusao(prazoConclusaoNovo);

        obra.PrazoConclusao.Should().Be(prazoConclusao);
    }
    
    [Fact]
    public void AlocarFuncionario_ComDadoValido_DeveRealizarOperacao()
    {
        var obra = ObraStub.Valido(_fixture);
        var funcionario = FuncionarioStub.Valido(_fixture);
        
        obra.AlocarFuncionario(funcionario);

        obra.Funcionarios.Should().NotBeNull();
        obra.Funcionarios.Should().Contain(funcionario);
    }

    [Fact]
    public void AlocarFuncionario_ComFuncionarioAlocado_DeveLancarExcecao()
    {
        var obra = ObraStub.Valido(_fixture);
        var funcionario = FuncionarioStub.Valido(_fixture);
        obra.AlocarFuncionario(funcionario);
        
        var result = () => obra.AlocarFuncionario(funcionario);

        result.Should().Throw<OperacaoInvalidaException>();
    }
    
    [Fact]
    public void DesalocarFuncionario_ComDadoValido_DeveRealizarOperacao()
    {
        var obra = ObraStub.Valido(_fixture);
        var funcionario = FuncionarioStub.Valido(_fixture);
        obra.AlocarFuncionario(funcionario);
        
        obra.DesalocarFuncionario(funcionario);

        obra.Funcionarios.Should().NotBeNull();
        obra.Funcionarios.Should().NotContain(funcionario);
    }

    [Fact]
    public void DesalocarFuncionario_ComFuncionarioNaoAlocado_DeveLancarExcecao()
    {
        var obra = ObraStub.Valido(_fixture);
        var funcionario = FuncionarioStub.Valido(_fixture);
        
        var result = () => obra.DesalocarFuncionario(funcionario);

        result.Should().Throw<OperacaoInvalidaException>();
    }

    [Fact]
    public void AlocarMaterial_ComDadoValido_DeveRealizarOperacao()
    {
        var quantidade = 1;

        var obra = ObraStub.Valido(_fixture);
        var material = new Material("teste", "teste", TipoMaterial.Cimento, 10.80, quantidade);
        
        obra.AlocarMaterial(material, quantidade);

        obra.ObraMateriais.Should().NotBeNull();
        obra.ObraMateriais!.Count.Should().Be(1);
        obra.ObraMateriais.First().Material.Should().BeEquivalentTo(material);
        obra.ObraMateriais.First().Operacao.Should().Be(EntradaSaida.Entrada);
        obra.ObraMateriais.First().Quantidade.Should().Be(quantidade);
    }
    
    [Fact]
    public void AlocarMaterial_ComMaterialSemEstoqueSuficiente_DeveLancarExcecao()
    {
        var obra = ObraStub.Valido(_fixture);
        var material = new Material("teste", "teste", TipoMaterial.Cimento, 10.80, 1);
        
        var result = () => obra.AlocarMaterial(material, 10);

        result.Should().Throw<OperacaoInvalidaException>();
    }
    
    [Fact]
    public void DesalocarMaterial_ComDadoValido_DeveRealizarOperacao()
    {
        var quantidade = 1;
        var obra = ObraStub.Valido(_fixture);
        var material = new Material("teste", "teste", TipoMaterial.Cimento, 10.80, quantidade);
        
        obra.AlocarMaterial(material, quantidade);
        
        obra.DesalocarMaterial(material, quantidade);

        obra.ObraMateriais!.Count.Should().Be(2);
        obra.ObraMateriais.Last().Material.Should().BeEquivalentTo(material);
        obra.ObraMateriais.Last().Operacao.Should().Be(EntradaSaida.Saida);
        obra.ObraMateriais.Last().Quantidade.Should().Be(quantidade);
    }
    
    [Fact]
    public void DesalocarMaterial_ComQuantidadeInvalida_DeveLancarExcecao()
    {
        var obra = ObraStub.Valido(_fixture);
        var material = new Material("teste", "teste", TipoMaterial.Cimento, 10.80, 1);
        obra.AlocarMaterial(material, 1);
        
        var result = () => obra.DesalocarMaterial(material, null);

        result.Should().Throw<OperacaoInvalidaException>();
    }
    
    [Fact]
    public void DesalocarMaterial_ComQuantidadeParaDesalocarMaiorQueAQuantidadeMateriaisAlocados_DeveLancarExcecao()
    {
        var obra = ObraStub.Valido(_fixture);
        var material = new Material("teste", "teste", TipoMaterial.Cimento, 10.80, 1);
        obra.AlocarMaterial(material, 1);
        
        var result = () => obra.DesalocarMaterial(material, 2);

        result.Should().Throw<OperacaoInvalidaException>();
    }
    
    [Fact]
    public void DesalocarMaterial_ComMaterialNaoAlocado_DeveLancarExcecao()
    {
        var obra = ObraStub.Valido(_fixture);
        var material = new Material("teste", "teste", TipoMaterial.Cimento, 10.80, 1);
        
        var result = () => obra.DesalocarMaterial(material, 1);

        result.Should().Throw<OperacaoInvalidaException>();
    }
}