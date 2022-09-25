using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Application.Interfaces
{
    public interface IMaterialService : IServiceBase<Material, MaterialDto>
    {
        void MovimentarEstoque(long id, EstoqueDto dto);
    }
}