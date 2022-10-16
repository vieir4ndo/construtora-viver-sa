using ConstrutoraViverSA.Domain;
using System.Collections.Generic;
using AutoMapper;
using ConstrutoraViverSA.Application.Interfaces;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Exceptions;
using ConstrutoraViverSA.Repository.Interfaces;

namespace ConstrutoraViverSA.Application.Services;

public class MaterialService : IMaterialService
{
    private readonly IMaterialRepository _repository;
    private readonly IMapper _mapper;

    public MaterialService(IMaterialRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public List<MaterialDto> BuscarTodos()
    {
        var materiais = _repository.BuscarTodos();

        var listaMateriaisDto = new List<MaterialDto>();
        
        materiais.ForEach(x => listaMateriaisDto.Add(_mapper.Map<MaterialDto>(x)));

        return listaMateriaisDto;
    }

    public Material BuscarEntidadePorId(long buscaId)
    {
        var material = _repository.BuscarPorId(buscaId);

        if (material is null)
        {
            throw new NaoEncontradoException("Material não encontrado");
        }

        return material;
    }

    public MaterialDto BuscarPorId(long buscaId)
    {
        var material = BuscarEntidadePorId(buscaId);

        return _mapper.Map<MaterialDto>(material);
    }

    public void Adicionar(MaterialDto dto)
    {
        var material = new Material(dto.Nome, dto.Descricao, dto.Tipo, dto.Valor,
            dto.Quantidade);
        _repository.Adicionar(material);
    }

    public void Excluir(long idExcluir)
    {
        var material = BuscarEntidadePorId(idExcluir);

        _repository.Excluir(material);
    }

    public void Editar(long id, EditarMaterialDto materialAtualizado)
    {
        var material = BuscarEntidadePorId(id);

        material.SetNome(materialAtualizado.Nome);
        material.SetDescricao(materialAtualizado.Descricao);
        material.SetValor(materialAtualizado.Valor);
        material.SetTipo(materialAtualizado.Tipo);

        _repository.Editar(material);
    }

    public void MovimentarEstoque(long id, EntradaSaidaMaterialDto materialDto)
    {
        var material = BuscarEntidadePorId(id);

        material.MovimentarEstoque(materialDto.Operacao, materialDto.Quantidade);

        _repository.Editar(material);
    }
}