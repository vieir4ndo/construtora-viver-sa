using System;
using ConstrutoraViverSA.Domain;
using System.Collections.Generic;
using ConstrutoraViverSA.Application.Interfaces;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Repository.Interfaces;

namespace ConstrutoraViverSA.Application.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _repository;
        public MaterialService(IMaterialRepository repository)
        {
            _repository = repository;
        }

        public List<Material> BuscarMateriais()
        {
            return _repository.BuscarMateriais();
        }

        public Material BuscarMaterialPorId(long buscaId)
        {
            var material = _repository.BuscarMaterialPorId(buscaId);
            
            if (material is null)
            {
                throw new Exception("Material não encontrado");
            }

            return material;
        }

        public void AdicionarMaterial(MaterialDto material)
        {
            _repository.AdicionarMaterial(material);
        }
        public void ExcluirMaterial(long idExcluir)
        {
            var material = BuscarMaterialPorId(idExcluir);

            _repository.ExcluirMaterial(material);
        }

        public void AlterarMaterial(long id, MaterialDto materialAtualizado)
        {
            var material = BuscarMaterialPorId(id);
            
            material.Nome = materialAtualizado.Nome ?? material.Nome;
            material.Descricao = materialAtualizado.Descricao ?? material.Descricao;
            material.Valor = materialAtualizado.Valor ?? material.Valor;
            material.Tipo = materialAtualizado.Tipo ?? material.Tipo;

            _repository.AlterarMaterial(material);
        }
    }
}
