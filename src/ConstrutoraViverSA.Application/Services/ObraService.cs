using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ConstrutoraViverSA.Application.Interfaces;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;
using ConstrutoraViverSA.Repository.Interfaces;

namespace ConstrutoraViverSA.Application.Services;

public class ObraService : IObraService
{
    private readonly IFuncionarioService _funcionarioService;
    private readonly IMaterialService _materialService;
    private readonly IOrcamentoService _orcamentoService;
    private readonly IObraRepository _repository;
    private readonly IObraParaObraDto _obraParaObraDto;

    public ObraService(
        IObraRepository repository,
        IOrcamentoService orcamentoService,
        IFuncionarioService funcionarioService,
        IMaterialService materialService,
        IObraParaObraDto obraParaObraDto)
    {
        _repository = repository;
        _orcamentoService = orcamentoService;
        _funcionarioService = funcionarioService;
        _materialService = materialService;
        _obraParaObraDto = obraParaObraDto;
    }

    public List<ObraDto> BuscarTodos()
    {
        var obras= _repository.BuscarTodos();

        var listaObrasDto = new List<ObraDto>();

        obras.ForEach(x => listaObrasDto.Add(_obraParaObraDto.Mapear(x)));

        return listaObrasDto;
    }

    public ObraDto BuscarPorId(long buscaId)
    {
        var obra = BuscarEntidadePorId(buscaId);

        return _obraParaObraDto.Mapear(obra);
    }
    
    public Obra BuscarEntidadePorId(long buscaId)
    {
        var obra = _repository.BuscarPorId(buscaId);

        if (obra is null) throw new NaoEncontradoException("Obra não encontrada");

        return obra;
    }

    public void Adicionar(ObraDto dto)
    {
        var orcamento = _orcamentoService.BuscarEntidadePorId((long)dto.OrcamentoId);
        
        var funcionarios = (dto.Funcionarios is not null) ? BuscarListaDeFuncionarios(dto.Funcionarios) : null;

        var materiais = (dto.Materiais is not null) ? BuscarDicionarioDeMateriaisEQuantidades(dto.Materiais) : null;
        
        var obra = new Obra(dto.Nome, dto.Endereco, dto.TipoObra, dto.Descricao, dto.Valor, dto.PrazoConclusao,
            orcamento, funcionarios, materiais);

        _repository.Adicionar(obra);
    }

    private List<Funcionario> BuscarListaDeFuncionarios(List<long> funcionariosId)
    {
        var funcionarios = new List<Funcionario>();

        foreach (var funcionarioId in funcionariosId)
        {
            funcionarios.Add(_funcionarioService.BuscarEntidadePorId(funcionarioId));
        }

        return funcionarios;
    }

    private Dictionary<Material, int> BuscarDicionarioDeMateriaisEQuantidades(Dictionary<long, int> dicionarioDeMateriais)
    {
        var materiais = new Dictionary<Material, int>();

        foreach (var material in dicionarioDeMateriais)
        {
            materiais.Add(_materialService.BuscarEntidadePorId(material.Key), material.Value);
        }

        return materiais;
    }

    public void Excluir(long idExcluir)
    {
        var obra = BuscarEntidadePorId(idExcluir);

        _repository.Excluir(obra);
    }

    public void Editar(long id, ObraDto obralAtualizado)
    {
        if (obralAtualizado.Funcionarios != null && obralAtualizado.Funcionarios.Count > 0)
            throw new OperacaoInvalidaException("Funcionarios não podem ser alterados na edição");
        
        if (obralAtualizado.Materiais != null && obralAtualizado.Materiais.Count > 0)
            throw new OperacaoInvalidaException("Materiais não podem ser alterados na edição");

        var obra = BuscarEntidadePorId(id);

        if (obralAtualizado.OrcamentoId != null && obralAtualizado.OrcamentoId != obra.OrcamentoId)
        {
            var orcamento = _orcamentoService.BuscarEntidadePorId((long)obralAtualizado.OrcamentoId);
            obra.SetOrcamento(orcamento);
        }

        obra.SetNome(obralAtualizado.Nome);
        obra.SetDescricao(obralAtualizado.Descricao);
        obra.SetEndereco(obralAtualizado.Endereco);
        obra.SetTipoObra(obralAtualizado.TipoObra);
        obra.SetValor(obralAtualizado.Valor);
        obra.SetPrazoConclusao(obralAtualizado.PrazoConclusao);

        _repository.Editar(obra);
    }

    public void GerenciarMaterial(EntradaSaidaMaterialDto materialDto, long id, long materialId)
    {
        if (materialDto.Operacao == EntradaSaida.Entrada)
            AlocarMaterial(materialDto, id, materialId);
        else
            DesalocarMaterial(materialDto, id, materialId);
    }
    
    private void AlocarMaterial(EntradaSaidaMaterialDto materialDto, long id, long materialId)
    {
        var material = _materialService.BuscarEntidadePorId(materialId);

        var obra = BuscarEntidadePorId(id);

        obra.AlocarMaterial(material, materialDto.Quantidade);

        _repository.Editar(obra);
    }

    private void DesalocarMaterial(EntradaSaidaMaterialDto materialDto, long id, long materialId)
    {
        var material = _materialService.BuscarEntidadePorId(materialId);

        var obra = BuscarEntidadePorId(id);
        
        obra.DesalocarMaterial(material, materialDto.Quantidade);

        _repository.Editar(obra);
    }

    public void AlocarFuncionario(long id, long funcionarioId)
    {
        var funcionario = _funcionarioService.BuscarEntidadePorId(funcionarioId);

        var obra = BuscarEntidadePorId(id);

        obra.AlocarFuncionario(funcionario);

        _repository.Editar(obra);
    }

    public void DesalocarFuncionario(long id, long funcionarioId)
    {
        var funcionario = _funcionarioService.BuscarEntidadePorId(funcionarioId);

        var obra = BuscarEntidadePorId(id);

        obra.DesalocarFuncionario(funcionario);

        _repository.Editar(obra);
    }
}