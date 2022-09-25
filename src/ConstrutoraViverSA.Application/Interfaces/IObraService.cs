using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Application.Interfaces
{
    public interface IObraService : IServiceBase<Obra, ObraDto>
    {
        void AlocarFuncionario(long id, long funcionarioId);
        void DesalocarFuncionario(long id, long funcionarioId);
    }
}