using System.Collections.Generic;
using AutoMapper;
using ConstrutoraViverSA.Application.Interfaces;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Exceptions;
using ConstrutoraViverSA.Repository.Interfaces;

namespace ConstrutoraViverSA.Application.Services;

public class ObraMaterialService : IObraMaterialService
{
    private readonly IObraMaterialRepository _repository;
    private readonly IMapper _mapper;

    public ObraMaterialService(IObraMaterialRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public List<ObraMaterialDto> BuscarTodos()
    {
        var obras = _repository.BuscarTodos();

        var listaObrasDto = new List<ObraMaterialDto>();

        obras.ForEach(x => listaObrasDto.Add(_mapper.Map<ObraMaterialDto>(x)));

        return listaObrasDto;
    }

    private ObraMaterial BuscarEntidadePorId(long buscaId)
    {
        var obraMaterial = _repository.BuscarPorId(buscaId);

        if (obraMaterial is null) throw new NaoEncontradoException("ObraMaterial não encontrado");

        return obraMaterial;
    }

    public ObraMaterialDto BuscarPorId(long buscaId)
    {
        var obraMaterial = BuscarEntidadePorId(buscaId);

        return _mapper.Map<ObraMaterialDto>(obraMaterial);
    }

    public void Adicionar(ObraMaterialDto dto)
    {
        var obraMaterial = _mapper.Map<ObraMaterial>(dto);
        
        _repository.Adicionar(obraMaterial);
    }

    public void Excluir(long idExcluir)
    {
        var obraMaterial = BuscarEntidadePorId(idExcluir);

        _repository.Excluir(obraMaterial);
    }

    public void Editar(long id, ObraMaterialDto dto)
    {
        var obraMaterial = BuscarEntidadePorId(id);

        if (dto.MaterialId != null && dto.MaterialId != obraMaterial.MaterialId)
            obraMaterial.MaterialId = dto.MaterialId;

        if (dto.ObraId != null && dto.ObraId != obraMaterial.ObraId) obraMaterial.ObraId = dto.ObraId;

        _repository.Editar(obraMaterial);
    }

    public ObraMaterial BuscarPorObraIdEMaterialId(long obraId, long materialId)
    {
        return _repository.BuscarPorObraIdEMaterialId(obraId, materialId);
    }
}