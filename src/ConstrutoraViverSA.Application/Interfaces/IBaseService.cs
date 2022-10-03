using System.Collections.Generic;

namespace ConstrutoraViverSA.Application.Interfaces;

public interface IBaseService<Dto>
{
    List<Dto> BuscarTodos();
    Dto BuscarPorId(long buscaId);
    void Adicionar(Dto dto);
    void Excluir(long idExcluir);
    void Editar(long id, Dto dto);
}