using System.Collections.Generic;
using System.Linq;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Infrastructure;
using ConstrutoraViverSA.Repository.Interfaces;

namespace ConstrutoraViverSA.Repository.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly ApplicationContext _database;

        public MaterialRepository(ApplicationContext applicationContext)
        {
            _database = applicationContext;
        }

        public List<Material> BuscarMateriais()
        {
            return _database.Materiais
                .Where(p => p.Id > 0)
                .OrderBy(p => p.Id)
                .ToList();
        }

        public Material BuscarMaterialPorId(long buscaId)
        {
            return _database.Materiais
                .FirstOrDefault(p => p.Id == buscaId);
        }

        public void AdicionarMaterial(MaterialDto material)
        {
            _database.Materiais.Add(material.DtoParaDominio());
            _database.SaveChanges();
        }

        public void ExcluirMaterial(Material material)
        {
            _database.Materiais.Remove(material);
            _database.SaveChanges();
        }

        public void AlterarMaterial(Material material)
        {
            _database.Materiais.Update(material);
            _database.SaveChanges();
        }
    }
}