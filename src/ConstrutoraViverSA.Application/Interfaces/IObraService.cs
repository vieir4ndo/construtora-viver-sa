using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Application.Interfaces
{
    public interface IObraService : IBaseService<Obra, ObraDto>
    {
        void AlocarFuncionario(long id, long funcionarioId);
        void DesalocarFuncionario(long id, long funcionarioId);
        void AlocarMaterial(long id, long materialId);
        void DesalocarMaterial(long id, long materialId);
    }
}