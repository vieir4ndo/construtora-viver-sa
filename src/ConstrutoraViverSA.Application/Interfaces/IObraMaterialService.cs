using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Application.Interfaces;

public interface IObraMaterialService
{
    ObraMaterial BuscarPorObraIdEMaterialId(long obraId, long materialId);
}