using System.Collections.Generic;
using System.Linq;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Infraestrutura;
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
        public List<Orcamento> BuscarOrcamentos()
        {
            return _database.Orcamentos
                .Where(p => p.Id > 0)
                .OrderBy(p => p.Id)
                .ToList();
        }

        public Orcamento BuscarOrcamentoPorId(long buscaId)
        {
            return _database.Orcamentos
                .FirstOrDefault(p => p.Id == buscaId);
        }

        public void AdicionarOrcamento(Orcamento orcamento)
        {
            _database.Orcamentos.Add(orcamento);
            _database.SaveChanges();
        }

        public void ExcluirOrcamento(Orcamento orcamento)
        {
            _database.Orcamentos.Remove(orcamento);
            _database.SaveChanges();
        }

        public void AlterarOrcamento(Orcamento orcamento)
        {
            _database.Orcamentos.Update(orcamento);
            _database.SaveChanges();
        }
    }
}