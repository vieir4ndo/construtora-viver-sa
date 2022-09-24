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
        public List<Obra> BuscarObras()
        {
            return _database.Obras
                .Where(p => p.Id > 0)
                .OrderBy(p => p.Id)
                .ToList();
        }

        public Obra BuscarObraPorId(long buscaId)
        {
            return _database.Obras
                .FirstOrDefault(p => p.Id == buscaId);
        }

        public void AdicionarObra(Obra obra)
        {
            _database.Obras.Add(obra);
            _database.SaveChanges();
        }

        public void ExcluirObra(Obra obra)
        {
            _database.Obras.Remove(obra);
            _database.SaveChanges();
        }

        public void AlterarObra(Obra obra)
        {
            _database.Obras.Update(obra);
            _database.SaveChanges();
        }
    }
}