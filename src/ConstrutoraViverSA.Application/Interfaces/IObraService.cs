using System.Collections.Generic;
using ConstrutoraViverSA.Domain;

namespace ConstrutoraViverSA.Application.Interfaces
{
    public interface IObraService
    {
        List<Obra> BuscarObras();
        Obra BuscarObraPorId(long buscaId);
        void AdicionarObra(Obra obra);
        void ExcluirObra(long idExcluir);
        void AlterarObra(long id, Obra obralAtualizado);
    }
}