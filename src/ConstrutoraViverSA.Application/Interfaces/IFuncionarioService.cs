using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Application.Interfaces;

public interface IFuncionarioService : IBaseService<FuncionarioDto>
{
    Funcionario BuscarEntidadePorId(long buscaId);
}