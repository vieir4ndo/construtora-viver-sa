using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Application.Interfaces;

public interface IObraService : IBaseService<ObraDto>
{
    void GerenciarMaterial(EntradaSaidaMaterialDto materialDto, long id, long materialId);
    void DesalocarFuncionario(long id, long funcionarioId);
    void AlocarFuncionario(long id, long funcionarioId);
    Obra BuscarEntidadePorId(long buscaId);
}