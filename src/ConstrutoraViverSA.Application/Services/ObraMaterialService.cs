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

    public ObraMaterial BuscarPorObraIdEMaterialId(long obraId, long materialId)
    {
        return _repository.BuscarPorObraIdEMaterialId(obraId, materialId);
    }
}