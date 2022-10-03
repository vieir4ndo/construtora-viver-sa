using System;
using ConstrutoraViverSA.Domain;
using System.Collections.Generic;
using AutoMapper;
using ConstrutoraViverSA.Application.Interfaces;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Enums;
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

    public void Adicionar(MaterialDto material)
    {
        _repository.Adicionar(_mapper.Map<Material>(material));
    }

    public void Excluir(long idExcluir)
    {
        var material = BuscarEntidadePorId(idExcluir);

        _repository.Excluir(material);
    }

    public void Editar(long id, MaterialDto materialAtualizado)
    {
        var material = BuscarEntidadePorId(id);

        material.Nome = materialAtualizado.Nome ?? material.Nome;
        material.Descricao = materialAtualizado.Descricao ?? material.Descricao;
        material.Valor = materialAtualizado.Valor ?? material.Valor;
        material.Tipo = materialAtualizado.Tipo ?? material.Tipo;

        _repository.Editar(material);
    }

    public void MovimentarEstoque(long id, EntradaSaidaMaterialDto materialDto)
    {
        var material = BuscarEntidadePorId(id);

        if (materialDto.Operacao == EntradaSaidaEnum.Saida && materialDto.Quantidade > material.Quantidade)
        {
            throw new OperacaoInvalidaException(
                $"Solicitou-se a baixa de {materialDto.Quantidade} itens do estoque, no entanto o material {material.Nome} possui apenas {material.Quantidade} itens em estoque");
        }

        materialDto.MaterialId = material.Id;

        material.Estoque.Add(_mapper.Map<Estoque>(materialDto));

        if (materialDto.Operacao == EntradaSaidaEnum.Entrada)
        {
            material.Quantidade += materialDto.Quantidade;
        }
        else
        {
            material.Quantidade -= materialDto.Quantidade;
        }

        _repository.Editar(material);
    }
}