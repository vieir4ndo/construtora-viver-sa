using System.Collections.Generic;

namespace ConstrutoraViverSA.Repository.Interfaces;

public interface IBaseRepository<T>
{
    List<T> BuscarTodos();
    T BuscarPorId(long buscaId);
    void Adicionar(T obj);
    void Excluir(T obj);
    void Editar(T obj);
}