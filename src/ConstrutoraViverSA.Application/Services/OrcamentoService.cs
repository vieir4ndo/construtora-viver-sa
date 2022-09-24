using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Infraestrutura;
using System.Collections.Generic;
using System.Linq;
using ConstrutoraViverSA.Application.Interfaces;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Application.Services
{
    public class OrcamentoService : IOrcamentoService
    {
        private readonly ApplicationContext _database;
        public OrcamentoService(ApplicationContext applicationContext)
        {
            _database = applicationContext;
        }

        public List<Orcamento> BuscarOrcamentos()
        {
            return _database.Orcamentos
                .Where(p => p.Id > 0)
                .OrderBy(p => p.Id)
                .ToList();
        }

        public Orcamento BuscarOrcamentoPorId(long buscaId)
        {
            return _database.Orcamentos
                .FirstOrDefault(p => p.Id == buscaId);
        }

        public void AdicionarOrcamento(OrcamentoDto dto)
        {
            // TODO: Levar lógica para repositório
            // TODO: Usar Automapper

            _database.Orcamentos.Add(dto.DtoParaDominio());
            _database.SaveChanges();
        }
        public void ExcluirOrcamento(long idExcluir)
        {
            Orcamento Orcamento = _database.Orcamentos.Find(idExcluir);

            _database.Orcamentos.Remove(Orcamento);
            _database.SaveChanges();
        }

        public void AlterarOrcamento(long id, OrcamentoDto orcamentolAtualizado)
        {
            Orcamento Orcamento = _database.Orcamentos.Find(id);

            Orcamento.Descricao = orcamentolAtualizado.Descricao;
            Orcamento.Endereco = orcamentolAtualizado.Endereco;
            Orcamento.TipoObra = orcamentolAtualizado.TipoObra;
            Orcamento.DataEmissao = orcamentolAtualizado.DataEmissao;
            Orcamento.DataValidade = orcamentolAtualizado.DataValidade;
            Orcamento.Valor = orcamentolAtualizado.Valor;

            _database.Orcamentos.Update(Orcamento);
            _database.SaveChanges();
        }
    }
}