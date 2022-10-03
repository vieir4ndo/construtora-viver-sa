using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Application.Interfaces;

public interface IMaterialService : IBaseService<MaterialDto>
{
    void MovimentarEstoque(long id, EntradaSaidaMaterialDto materialDto);
    Material BuscarEntidadePorId(long buscaId);
}