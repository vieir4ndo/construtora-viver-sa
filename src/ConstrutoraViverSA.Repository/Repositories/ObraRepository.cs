using System.Collections.Generic;
using System.Linq;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Infrastructure;
using ConstrutoraViverSA.Repository.Interfaces;

namespace ConstrutoraViverSA.Repository.Repositories
{
    public class ObraRepository: IObraRepository
    {
        private readonly ApplicationContext _database;

        public ObraRepository(ApplicationContext applicationContext)
        {
            _database = applicationContext;
        }
        public List<Obra> BuscarTodos()
        {
            return _database.Obras
                .Where(p => p.Id > 0)
                .OrderBy(p => p.Id)
                .ToList();
        }

        public Obra BuscarPorId(long buscaId)
        {
            return _database.Obras
                .FirstOrDefault(p => p.Id == buscaId);
        }

        public void Adicionar(Obra obra)
        {
            _database.Obras.Add(obra);
            _database.SaveChanges();
        }

        public void Excluir(Obra obra)
        {
            _database.Obras.Remove(obra);
            _database.SaveChanges();
        }

        public void Editar(Obra obra)
        {
            _database.Obras.Update(obra);
            _database.SaveChanges();
        }
    }
}