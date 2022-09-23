using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Infraestrutura;
using System.Collections.Generic;
using System.Linq;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Service
{
    public class MaterialService
    {
        private readonly ApplicationContext _database;

        public MaterialService(ApplicationContext applicationContext)
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

        public Material BuscarMaterialPorId(long BuscaId)
        {
            return _database.Materiais
                .FirstOrDefault(p => p.Id == BuscaId);
        }

        public void AdicionarMaterial(MaterialDto material)
        {
            _database.Materiais.Add(material.DtoParaDominio());
            _database.SaveChanges();
        }
        public void ExcluirMaterial(long IdExcluir)
        {
            Material material = _database.Materiais.Find(IdExcluir);

            _database.Materiais.Remove(material);
            _database.SaveChanges();
        }

        public void AlterarMaterial(long Id, MaterialDto materialAtualizado)
        {
            Material material = _database.Materiais.Find(Id);

            material.Nome = materialAtualizado.Nome;
            material.Descricao = materialAtualizado.Descricao;
            material.Valor = materialAtualizado.Valor;
            material.DataValidade = materialAtualizado.DataValidade;
            material.Tipo = materialAtualizado.Tipo;

            _database.Materiais.Update(material);
            _database.SaveChanges();
        }
    }
}
