using System.Collections.Generic;
using ConstrutoraViverSA.Domain;

namespace ConstrutoraViverSA.Repository.Interfaces
{
    public interface IObraRepository
    {
        List<Obra> BuscarObras();
        Obra BuscarObraPorId(long buscaId);
        void AdicionarObra(Obra obra);
        void ExcluirObra(Obra obra);
        void AlterarObra(Obra obra);
        
    }
}