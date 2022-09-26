using System.Collections.Generic;
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

    public ObraService(
        IObraRepository repository,
        IOrcamentoService orcamentoService,
        IFuncionarioService funcionarioService,
        IMaterialService materialService,
        IObraMaterialService obraMaterialService)
    {
        _repository = repository;
        _orcamentoService = orcamentoService;
        _funcionarioService = funcionarioService;
        _materialService = materialService;
        _obraMaterialService = obraMaterialService;
    }

    public List<Obra> BuscarTodos()
    {
        return _repository.BuscarTodos();
    }

    public Obra BuscarPorId(long buscaId)
    {
        var obra = _repository.BuscarPorId(buscaId);

        if (obra is null) throw new NaoEncontradoException("Obra não encontrada");

        return obra;
    }

    public void Adicionar(ObraDto dto)
    {
        var orcamento = _orcamentoService.BuscarPorId((long)dto.OrcamentoId);
        var obra = dto.DtoParaDominio();

        obra.Orcamento = orcamento;

        _repository.Adicionar(obra);
    }

    public void Excluir(long idExcluir)
    {
        var obra = BuscarPorId(idExcluir);

        _repository.Excluir(obra);
    }

    public void Editar(long id, ObraDto obralAtualizado)
    {
        var obra = BuscarPorId(id);

        if (obralAtualizado.OrcamentoId != null && obralAtualizado.OrcamentoId != obra.OrcamentoId)
        {
            var orcamento = _orcamentoService.BuscarPorId((long)obralAtualizado.OrcamentoId);
            obra.Orcamento = orcamento;
        }

        obra.Nome = obralAtualizado.Nome ?? obra.Nome;
        obra.Descricao = obralAtualizado.Descricao ?? obra.Descricao;
        obra.Endereco = obralAtualizado.Endereco ?? obra.Endereco;
        obra.TipoObra = obralAtualizado.TipoObra ?? obra.TipoObra;
        obra.Valor = obralAtualizado.Valor ?? obra.Valor;
        obra.PrazoConclusao = obralAtualizado.PrazoConclusao ?? obra.PrazoConclusao;
        obra.OrcamentoId = obralAtualizado.OrcamentoId ?? obra.OrcamentoId;

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
        var funcionario = _funcionarioService.BuscarPorId(funcionarioId);

        var obra = BuscarPorId(id);

        if (obra.Funcionarios.Contains(funcionario))
            throw new OperacaoInvalidaException(
                $"Funcionário {funcionario.Nome} já está alocado na obra {obra.Nome}");

        obra.Funcionarios.Add(funcionario);

        _repository.Editar(obra);
    }

    public void DesalocarFuncionario(long id, long funcionarioId)
    {
        var funcionario = _funcionarioService.BuscarPorId(funcionarioId);

        var obra = BuscarPorId(id);

        if (!obra.Funcionarios.Contains(funcionario))
            throw new OperacaoInvalidaException(
                $"Funcionário {funcionario.Nome} não está alocado na obra {obra.Nome}");

        obra.Funcionarios.Remove(funcionario);

        _repository.Editar(obra);
    }

    private void AlocarMaterial(EntradaSaidaMaterialDto materialDto, long id, long materialId)
    {
        var material = _materialService.BuscarPorId(materialId);

        var obra = BuscarPorId(id);

        if (material.Quantidade < materialDto.Quantidade)
        {
            throw new OperacaoInvalidaException(
                $"Não há itens suficientes em estoque do material {material.Nome} para alocar na obra {obra.Nome}");
        }

        var obraMaterial = _obraMaterialService.BuscarPorObraIdEMaterialId(id, materialId);

        _materialService.MovimentarEstoque(materialId,
            new EntradaSaidaMaterialDto() { Operacao = EntradaSaidaEnum.Saida, Quantidade = materialDto.Quantidade });

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

            obra.ObraMateriais.Add(obraMaterialDto.DtoParaDominio());
        }
        else
        {
            obraMaterial.Quantidade += materialDto.Quantidade;
        }

        _repository.Editar(obra);
    }

    private void DesalocarMaterial(EntradaSaidaMaterialDto materialDto, long id, long materialId)
    {
        var material = _materialService.BuscarPorId(materialId);

        var obra = BuscarPorId(id);

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