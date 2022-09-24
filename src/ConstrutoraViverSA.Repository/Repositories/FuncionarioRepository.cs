using System.Collections.Generic;
using System.Linq;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Infrastructure;
using ConstrutoraViverSA.Repository.Interfaces;

namespace ConstrutoraViverSA.Repository.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly ApplicationContext _database;

        public FuncionarioRepository(ApplicationContext applicationContext)
        {
            _database = applicationContext;
        }
        
        public List<Funcionario> BuscarFuncionarios()
        {
            return _database.Funcionarios
                .Where(p => p.Id > 0)
                .OrderBy(p => p.Id)
                .ToList();
        }
        
        public Funcionario BuscarFuncionarioPorId(long buscaId)
        {
            return _database.Funcionarios
                .FirstOrDefault(p => p.Id == buscaId);
        }
        
        public void AdicionarFuncionario(Funcionario funcionario)
        {
            _database.Funcionarios.Add(funcionario);
            _database.SaveChanges();
        }

        public void ExcluirFuncionario(Funcionario funcionario)
        {
            _database.Funcionarios.Remove(funcionario);
            _database.SaveChanges();
        }
        
        public void AlterarFuncionario(Funcionario funcionario)
        {
            _database.Funcionarios.Update(funcionario);
            _database.SaveChanges();
        }
    }
}