using System.Collections.Generic;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Application.Interfaces
{
    public interface IFuncionarioService
    {
        List<Funcionario> BuscarFuncionarios();
        Funcionario BuscarFuncionarioPorId(long buscaId);
        void AdicionarFuncionario(FuncionarioDto dto);
        void ExcluirFuncionario(long idExcluir);
        void AlterarFuncionario(long id, FuncionarioDto funcionariolAtualizado);
    }
}