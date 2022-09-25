using System.Collections.Generic;
using System.Linq;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Infrastructure;
using ConstrutoraViverSA.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            return _database.Obra
                .Where(p => p.Id > 0)
                .OrderBy(p => p.Id)
                .ToList();
        }

        public Obra BuscarPorId(long buscaId)
        {
            return _database.Obra
                .Where(p => p.Id == buscaId)
                .Include(p => p.Orcamento)
                .Include(p => p.Funcionarios)
                .Include(p => p.ObraMateriais)
                .FirstOrDefault();
        }

        public void Adicionar(Obra obra)
        {
            _database.Obra.Add(obra);
            _database.SaveChanges();
        }

        public void Excluir(Obra obra)
        {
            _database.Obra.Remove(obra);
            _database.SaveChanges();
        }

        public void Editar(Obra obra)
        {
            _database.Obra.Update(obra);
            _database.SaveChanges();
        }
    }
}