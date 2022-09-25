using ConstrutoraViverSA.Domain;

namespace ConstrutoraViverSA.Repository.Interfaces
{
    public interface IObraMaterialRepository : IBaseRepository<ObraMaterial>
    {
        ObraMaterial BuscarPorObraIdEMaterialId(long obraId, long materialId);
    }
}