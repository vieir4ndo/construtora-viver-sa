using System;
using ConstrutoraViverSA.Domain;
using System.Collections.Generic;
using ConstrutoraViverSA.Application.Interfaces;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Repository.Interfaces;

namespace ConstrutoraViverSA.Application.Services
{
    public class OrcamentoService : IOrcamentoService
    {
        private readonly IOrcamentoRepository _repository;
        public OrcamentoService(IOrcamentoRepository repository)
        {
            _repository = repository;
        }

        public List<Orcamento> BuscarOrcamentos()
        {
            return _repository.BuscarOrcamentos();
        }

        public Orcamento BuscarOrcamentoPorId(long buscaId)
        {
            var orcamento = _repository.BuscarOrcamentoPorId(buscaId);
            
            if (orcamento is null)
            {
                throw new Exception("Orçamento não encontrado");
            }

            return orcamento;
        }

        public void AdicionarOrcamento(OrcamentoDto dto)
        {
            _repository.AdicionarOrcamento(dto.DtoParaDominio());
        }
        public void ExcluirOrcamento(long idExcluir)
        {
            var orcamento = BuscarOrcamentoPorId(idExcluir);

            _repository.ExcluirOrcamento(orcamento);
        }

        public void AlterarOrcamento(long id, OrcamentoDto orcamentolAtualizado)
        {
            var orcamento = BuscarOrcamentoPorId(id);

            orcamento.Descricao = orcamentolAtualizado.Descricao ?? orcamento.Descricao;
            orcamento.Endereco = orcamentolAtualizado.Endereco ?? orcamento.Endereco;
            orcamento.TipoObra = orcamentolAtualizado.TipoObra ?? orcamento.TipoObra;
            orcamento.DataEmissao = orcamentolAtualizado.DataEmissao ?? orcamento.DataEmissao;
            orcamento.DataValidade = orcamentolAtualizado.DataValidade ?? orcamento.DataValidade;
            orcamento.Valor = orcamentolAtualizado.Valor;

           _repository.AlterarOrcamento(orcamento);
        }
    }
}