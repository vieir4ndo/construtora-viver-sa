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
        public MaterialService(IMaterialRepository repository)
        {
            _repository = repository;
        }

        public List<Material> BuscarTodos()
        {
            return _repository.BuscarTodos();
        }

        public Material BuscarPorId(long buscaId)
        {
            var material = _repository.BuscarPorId(buscaId);
            
            if (material is null)
            {
                throw new NaoEncontradoException("Material não encontrado");
            }

            return material;
        }

        public void Adicionar(MaterialDto material)
        {
            _repository.Adicionar(material.DtoParaDominio());
        }
        public void Excluir(long idExcluir)
        {
            var material = BuscarPorId(idExcluir);

            _repository.Excluir(material);
        }

        public void Editar(long id, MaterialDto materialAtualizado)
        {
            var material = BuscarPorId(id);
            
            material.Nome = materialAtualizado.Nome ?? material.Nome;
            material.Descricao = materialAtualizado.Descricao ?? material.Descricao;
            material.Valor = materialAtualizado.Valor ?? material.Valor;
            material.Tipo = materialAtualizado.Tipo ?? material.Tipo;

            _repository.Editar(material);
        }

        public void MovimentarEstoque(long id, EntradaSaidaMaterialDto materialDto)
        {
            var material = BuscarPorId(id);

            if (materialDto.EntradaSaidaEnum == EntradaSaidaEnum.Saida && materialDto.Quantidade > material.Quantidade)
            {
                throw new OperacaoInvalidaException(
                    $"Solicitou-se a baixa de {materialDto.Quantidade} itens do estoque, no entanto o material {material.Nome} possui apenas {material.Quantidade} itens em estoque");
            }

            materialDto.MaterialId = material.Id;
            
            material.Estoque.Add(materialDto.DtoParaDominio());
            
            if (materialDto.EntradaSaidaEnum == EntradaSaidaEnum.Entrada)
            {
                material.Quantidade += materialDto.Quantidade;
            }
            else
            {
                material.Quantidade -= materialDto.Quantidade;
            }
            
            _repository.Editar(material);
        }
    }
}
