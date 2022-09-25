using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Infrastructure;
using ConstrutoraViverSA.Repository.Interfaces;

namespace ConstrutoraViverSA.Repository.Repositories
{
    public class EstoqueRepository : IEstoqueRepository
    {
        private readonly ApplicationContext _database;

        public EstoqueRepository(ApplicationContext applicationContext)
        {
            _database = applicationContext;
        }
        
        public void AdicionarEstoque(Estoque estoque)
        {
            _database.Estoque.Add(estoque);
            _database.SaveChanges();
        }
    }
}