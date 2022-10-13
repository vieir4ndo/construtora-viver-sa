using System.Collections.Generic;
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
    private readonly IObraMaterialService _obraMaterialService;
    private readonly IOrcamentoService _orcamentoService;
    private readonly IObraRepository _repository;
    private readonly IMapper _mapper;

    public ObraService(
        IObraRepository repository,
        IOrcamentoService orcamentoService,
        IFuncionarioService funcionarioService,
        IMaterialService materialService,
        IObraMaterialService obraMaterialService,
        IMapper mapper)
    {
        _repository = repository;
        _orcamentoService = orcamentoService;
        _funcionarioService = funcionarioService;
        _materialService = materialService;
        _obraMaterialService = obraMaterialService;
        _mapper = mapper;
    }

    public List<ObraDto> BuscarTodos()
    {
        var obras= _repository.BuscarTodos();

        var listaObrasDto = new List<ObraDto>();
        
        obras.ForEach(x => listaObrasDto.Add(_mapper.Map<ObraDto>(x)));

        return listaObrasDto;
    }

    public ObraDto BuscarPorId(long buscaId)
    {
        var material = BuscarEntidadePorId(buscaId);

        return _mapper.Map<ObraDto>(material);
        
    }
    
    private Obra BuscarEntidadePorId(long buscaId)
    {
        var obra = _repository.BuscarPorId(buscaId);

        if (obra is null) throw new NaoEncontradoException("Obra não encontrada");

        return obra;
    }

    public void Adicionar(ObraDto dto)
    {
        var orcamento = _orcamentoService.BuscarEntidadePorId((long)dto.OrcamentoId);
        var obra = _mapper.Map<Obra>(dto);

        obra.SetOrcamento(orcamento);

        _repository.Adicionar(obra);
    }

    public void Excluir(long idExcluir)
    {
        var obra = BuscarEntidadePorId(idExcluir);

        _repository.Excluir(obra);
    }

    public void Editar(long id, ObraDto obralAtualizado)
    {
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
        if (materialDto.Operacao == EntradaSaidaEnum.Entrada)
            AlocarMaterial(materialDto, id, materialId);
        else
            DesalocarMaterial(materialDto, id, materialId);
    }

    public void AlocarFuncionario(long id, long funcionarioId)
    {
        var funcionario = _funcionarioService.BuscarEntidadePorId(funcionarioId);

        var obra = BuscarEntidadePorId(id);

        if (obra.Funcionarios.Contains(funcionario))
            throw new OperacaoInvalidaException(
                $"Funcionário {funcionario.Nome} já está alocado na obra {obra.Nome}");

        obra.Funcionarios.Add(funcionario);

        _repository.Editar(obra);
    }

    public void DesalocarFuncionario(long id, long funcionarioId)
    {
        var funcionario = _funcionarioService.BuscarEntidadePorId(funcionarioId);

        var obra = BuscarEntidadePorId(id);

        if (!obra.Funcionarios.Contains(funcionario))
            throw new OperacaoInvalidaException(
                $"Funcionário {funcionario.Nome} não está alocado na obra {obra.Nome}");

        obra.Funcionarios.Remove(funcionario);

        _repository.Editar(obra);
    }

    private void AlocarMaterial(EntradaSaidaMaterialDto materialDto, long id, long materialId)
    {
        var material = _materialService.BuscarEntidadePorId(materialId);

        var obra = BuscarEntidadePorId(id);

        if (material.Quantidade < materialDto.Quantidade)
        {
            throw new OperacaoInvalidaException(
                $"Não há itens suficientes em estoque do material {material.Nome} para alocar na obra {obra.Nome}");
        }

        var obraMaterial = _obraMaterialService.BuscarPorObraIdEMaterialId(id, materialId);

        if (obraMaterial == null)
        {
            var obraMaterialDto = new ObraMaterialDto
            {
                Material = material,
                MaterialId = material.Id,
                Obra = obra,
                ObraId = obra.Id,
                Quantidade = materialDto.Quantidade
            };

            obra.ObraMateriais.Add(_mapper.Map<ObraMaterial>(obraMaterialDto));
        }
        else
        {
            obraMaterial.SetQuantidade(materialDto.Quantidade);
        }
        
        _materialService.MovimentarEstoque(materialId,
            new EntradaSaidaMaterialDto() { Operacao = EntradaSaidaEnum.Saida, Quantidade = materialDto.Quantidade });

        _repository.Editar(obra);
    }

    private void DesalocarMaterial(EntradaSaidaMaterialDto materialDto, long id, long materialId)
    {
        var material = _materialService.BuscarPorId(materialId);

        var obra = BuscarEntidadePorId(id);

        var obraMaterial = _obraMaterialService.BuscarPorObraIdEMaterialId(id, materialId);

        if (obraMaterial == null)
            throw new OperacaoInvalidaException(
                $"Material {material.Nome} não está alocado na obra {obra.Nome}");

        if (obraMaterial.Quantidade < materialDto.Quantidade)
            throw new OperacaoInvalidaException(
                $"Material {material.Nome} está alocado na obra {obra.Nome} com apenas {obraMaterial.Quantidade} itens");

        _materialService.MovimentarEstoque(materialId,
            new EntradaSaidaMaterialDto() { Operacao = EntradaSaidaEnum.Entrada, Quantidade = materialDto.Quantidade });

        obra.ObraMateriais.Remove(obraMaterial);

        _repository.Editar(obra);
    }
}