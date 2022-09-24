using System.Collections.Generic;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Application.Interfaces
{
    public interface IOrcamentoService
    {
        List<Orcamento> BuscarOrcamentos();
        Orcamento BuscarOrcamentoPorId(long buscaId);
        void AdicionarOrcamento(OrcamentoDto dto);
        void ExcluirOrcamento(long idExcluir);
        void AlterarOrcamento(long id, OrcamentoDto orcamentolAtualizado);
    }
}