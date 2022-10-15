using ConstrutoraViverSA.Application.Interfaces;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Repository.Interfaces;

namespace ConstrutoraViverSA.Application.Services;

public class ObraMaterialService : IObraMaterialService
{
    private readonly IObraMaterialRepository _repository;

    public ObraMaterialService(IObraMaterialRepository repository)
    {
        _repository = repository;
    }

    public ObraMaterial BuscarPorObraIdEMaterialId(long obraId, long materialId)
    {
        return _repository.BuscarPorObraIdEMaterialId(obraId, materialId);
    }
}