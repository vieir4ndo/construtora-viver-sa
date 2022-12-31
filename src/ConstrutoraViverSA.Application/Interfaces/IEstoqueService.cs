using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Application.Interfaces
{
    public interface IEstoqueService
    {
        void MovimentarEstoque(EstoqueDto dto);
    }
}