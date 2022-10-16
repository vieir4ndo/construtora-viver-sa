using ConstrutoraViverSA.Domain;

namespace ConstrutoraViverSA.Repository.Interfaces;

public interface IObraMaterialRepository : IBaseRepository<ObraMaterial>
{
    int BuscarQuantidadeDeMateriaisPorObraIdEMaterialId(long obraId, long materialId);
}