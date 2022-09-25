using System.Collections.Generic;

namespace ConstrutoraViverSA.Application.Interfaces
{
    public interface IServiceBase<T, Dto>
    {
        List<T> BuscarTodos();
        T BuscarPorId(long buscaId);
        void Adicionar(Dto dto);
        void Excluir(long idExcluir);
        void Editar(long id, Dto dto);
    }
}