using System.Collections.Generic;
using ConstrutoraViverSA.Domain;

namespace ConstrutoraViverSA.Repository.Interfaces
{
    public interface IOrcamentoRepository
    {
        List<Orcamento> BuscarOrcamentos();
        Orcamento BuscarOrcamentoPorId(long buscaId);
        void AdicionarOrcamento(Orcamento orcamento);
        void ExcluirOrcamento(Orcamento orcamento);
        void AlterarOrcamento(Orcamento orcamento);
    }
}