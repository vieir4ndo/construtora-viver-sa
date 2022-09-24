using System.Collections.Generic;
using ConstrutoraViverSA.Domain;

namespace ConstrutoraViverSA.Repository.Interfaces
{
    public interface IFuncionarioRepository
    {
        List<Funcionario> BuscarFuncionarios();
        Funcionario BuscarFuncionarioPorId(long buscaId);
        void AdicionarFuncionario(Funcionario funcionario);
        void ExcluirFuncionario(Funcionario funcionario);
        void AlterarFuncionario(Funcionario funcionario);
    }
}