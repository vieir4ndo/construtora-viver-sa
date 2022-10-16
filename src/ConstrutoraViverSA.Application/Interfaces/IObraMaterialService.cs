namespace ConstrutoraViverSA.Application.Interfaces;

public interface IObraMaterialService
{
    int BuscarQuantidadeDeMateriaisPorObraIdEMaterialId(long obraId, long materialId);
}