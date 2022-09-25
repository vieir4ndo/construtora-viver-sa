using System;
using ConstrutoraViverSA.Domain;
using System.Collections.Generic;
using ConstrutoraViverSA.Application.Interfaces;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;
using ConstrutoraViverSA.Repository.Interfaces;

namespace ConstrutoraViverSA.Application.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _repository;
        private readonly IEstoqueService _estoqueService;
        public MaterialService(IMaterialRepository repository, IEstoqueService estoqueService)
        {
            _repository = repository;
            _estoqueService = estoqueService;
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
                throw new NaoEncontradoException("Material não encontrado");
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

        public void MovimentarEstoque(long id, EstoqueDto dto)
        {
            var material = BuscarMaterialPorId(id);

            if (dto.OperacaoEstoque == OperacaoEstoque.Saida && dto.Quantidade > material.Quantidade)
            {
                throw new OperacaoInvalidaException(
                    $"Solicitou-se a baixa de {dto.Quantidade} itens do estoque, no entanto o material {material.Nome} possui apenas {material.Quantidade} itens em estoque");
            }

            dto.MaterialId = material.Id;
            
            _estoqueService.MovimentarEstoque(dto);

            if (dto.OperacaoEstoque == OperacaoEstoque.Entrada)
            {
                material.Quantidade += dto.Quantidade;
            }
            else
            {
                material.Quantidade -= dto.Quantidade;
            }
            
            _repository.AlterarMaterial(material);
        }
    }
}
