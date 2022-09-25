using System.Collections.Generic;
using System.Linq;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Infrastructure;
using ConstrutoraViverSA.Repository.Interfaces;

namespace ConstrutoraViverSA.Repository.Repositories
{
    public class OrcamentoRepository : IOrcamentoRepository
    {
        private readonly ApplicationContext _database;

        public OrcamentoRepository(ApplicationContext applicationContext)
        {
            _database = applicationContext;
        }
        public List<Orcamento> BuscarTodos()
        {
            return _database.Orcamento
                .Where(p => p.Id > 0)
                .OrderBy(p => p.Id)
                .ToList();
        }

        public Orcamento BuscarPorId(long buscaId)
        {
            return _database.Orcamento
                .FirstOrDefault(p => p.Id == buscaId);
        }

        public void Adicionar(Orcamento obj)
        {
            _database.Orcamento.Add(obj);
            _database.SaveChanges();
        }

        public void Excluir(Orcamento obj)
        {
            _database.Orcamento.Remove(obj);
            _database.SaveChanges();
        }

        public void Editar(Orcamento obj)
        {
            _database.Orcamento.Update(obj);
            _database.SaveChanges();
        }
    }
}