using System.Collections.Generic;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Application.Interfaces;

public interface IMaterialService
{
    List<MaterialDto> BuscarTodos();
    MaterialDto BuscarPorId(long buscaId);
    void Adicionar(MaterialDto dto);
    void Excluir(long idExcluir);
    void MovimentarEstoque(long id, EntradaSaidaMaterialDto materialDto);
    Material BuscarEntidadePorId(long buscaId);
    void Editar(long id, EditarMaterialDto materialAtualizado);
}