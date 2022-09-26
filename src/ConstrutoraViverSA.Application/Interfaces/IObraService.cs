using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Application.Interfaces;

public interface IObraService : IBaseService<Obra, ObraDto>
{
    void GerenciarMaterial(EntradaSaidaMaterialDto materialDto, long id, long materialId);
    void DesalocarFuncionario(long id, long funcionarioId);
    void AlocarFuncionario(long id, long funcionarioId);
}