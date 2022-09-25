using ConstrutoraViverSA.Domain;

namespace ConstrutoraViverSA.Repository.Interfaces
{
    public interface IEstoqueRepository
    {
        void AdicionarEstoque(Estoque estoque);
    }
}