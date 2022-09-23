using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Infraestrutura;
using System.Collections.Generic;
using System.Linq;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Service
{
    public class OrcamentoService
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

        public Orcamento BuscarOrcamentoPorId(long BuscaId)
        {
            return _database.Orcamentos
                .FirstOrDefault(p => p.Id == BuscaId);
        }

        public void AdicionarOrcamento(OrcamentoDto dto)
        {
            // TODO: Levar lógica para repositório
            // TODO: Usar Automapper

            _database.Orcamentos.Add(dto.DtoParaDominio());
            _database.SaveChanges();
        }
        public void ExcluirOrcamento(long IdExcluir)
        {
            Orcamento Orcamento = _database.Orcamentos.Find(IdExcluir);

            _database.Orcamentos.Remove(Orcamento);
            _database.SaveChanges();
        }

        public void AlterarOrcamento(long Id, Orcamento OrcamentolAtualizado)
        {
            Orcamento Orcamento = _database.Orcamentos.Find(Id);

            Orcamento.Descricao = OrcamentolAtualizado.Descricao;
            Orcamento.Endereco = OrcamentolAtualizado.Endereco;
            Orcamento.TipoObra = OrcamentolAtualizado.TipoObra;
            Orcamento.DataEmissao = OrcamentolAtualizado.DataEmissao;
            Orcamento.DataValidade = OrcamentolAtualizado.DataValidade;
            Orcamento.Valor = OrcamentolAtualizado.Valor;

            _database.Orcamentos.Update(Orcamento);
            _database.SaveChanges();
        }
    }
}