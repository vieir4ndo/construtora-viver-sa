using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Application.Interfaces;

public interface IOrcamentoService : IBaseService<OrcamentoDto>
{
    Orcamento BuscarEntidadePorId(long buscaId);
}