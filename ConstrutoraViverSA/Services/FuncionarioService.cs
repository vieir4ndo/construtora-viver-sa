using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Infraestrutura;
using System.Collections.Generic;
using System.Linq;

namespace ConstrutoraViverSA.Services
{
    public class FuncionarioService
    {
        private readonly ApplicationContext _database;

        public FuncionarioService()
        {
            _database = new ApplicationContext();
        }

        public List<Funcionario> BuscarFuncionarios()
        {
            return _database.Funcionarios
                .Where(p => p.Id > 0)
                .OrderBy(p => p.Id)
                .ToList();
        }

        public Funcionario BuscarFuncionarioPorId(long BuscaId)
        {
            return _database.Funcionarios
                .FirstOrDefault(p => p.Id == BuscaId);
        }

        public void AdicionarFuncionario(Funcionario funcionario)
        {
            _database.Funcionarios.Add(funcionario);
            _database.SaveChanges();
        }
        public void ExcluirFuncionario(long IdExcluir)
        {
            Funcionario funcionario = _database.Funcionarios.Find(IdExcluir);

            _database.Funcionarios.Remove(funcionario);
            _database.SaveChanges();
        }

        public void AlterarFuncionario(long Id, Funcionario funcionariolAtualizado)
        {
            Funcionario funcionario = _database.Funcionarios.Find(Id);

            funcionario.Nome = funcionariolAtualizado.Nome;
            funcionario.DataNascimento = funcionariolAtualizado.DataNascimento;
            funcionario.Genero = funcionariolAtualizado.Genero;
            funcionario.Cpf = funcionariolAtualizado.Cpf;
            funcionario.NumCtps = funcionariolAtualizado.NumCtps;
            funcionario.Endereco = funcionariolAtualizado.Endereco;
            funcionario.Email = funcionariolAtualizado.Email;
            funcionario.Telefone = funcionariolAtualizado.Telefone;
            funcionario.Cargo = funcionariolAtualizado.Cargo;

            _database.Funcionarios.Update(funcionario);
            _database.SaveChanges();
        }
    }
}
