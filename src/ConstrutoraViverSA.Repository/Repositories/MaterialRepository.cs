using System.Collections.Generic;
using System.Linq;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Infrastructure;
using ConstrutoraViverSA.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConstrutoraViverSA.Repository.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly ApplicationContext _database;

        public MaterialRepository(ApplicationContext applicationContext)
        {
            _database = applicationContext;
        }

        public List<Material> BuscarTodos()
        {
            return _database.Material
                .Where(p => p.Id > 0)
                .OrderBy(p => p.Id)
                .ToList();
        }

        public Material BuscarPorId(long buscaId)
        {
            return _database.Material
                .Where(p => p.Id == buscaId)
                .Include(p => p.Estoque)
                .FirstOrDefault();
        }

        public void Adicionar(Material obj)
        {
            _database.Material.Add(obj);
            _database.SaveChanges();
        }

        public void Excluir(Material obj)
        {
            _database.Material.Remove(obj);
            _database.SaveChanges();
        }

        public void Editar(Material obj)
        {
            _database.Material.Update(obj);
            _database.SaveChanges();
        }
    }
}