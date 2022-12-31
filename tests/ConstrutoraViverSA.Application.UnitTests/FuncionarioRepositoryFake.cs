using System.Collections.Generic;
using System.Linq;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Repository.Interfaces;

namespace ConstrutoraViverSA.Application.UnitTests;

public class FuncionarioRepositoryFake : IFuncionarioRepository
{
    public List<Funcionario> Funcionarios { get; private set; }

    public FuncionarioRepositoryFake()
    {
        Funcionarios = new List<Funcionario>();
    }
    public List<Funcionario> BuscarTodos()
    {
        return Funcionarios;
    }

    public Funcionario BuscarPorId(long buscaId)
    {
        var funcionario = Funcionarios.FirstOrDefault(x => x.Id == buscaId);
        
        return funcionario;
    }

    public void Adicionar(Funcionario obj)
    {
        Funcionarios.Add(obj);
    }

    public void Excluir(Funcionario obj)
    {
        var funcionario = Funcionarios.FirstOrDefault(x => x.Id == obj.Id);
        
        Funcionarios.Remove(funcionario);
    }

    public void Editar(Funcionario obj)
    {
        var funcionario = Funcionarios.FirstOrDefault(x => x.Id == obj.Id);

        funcionario.SetNome(obj.Nome);
        funcionario.SetDataNascimento(obj.DataNascimento);
        funcionario.SetGenero(obj.Genero);
        funcionario.SetCpf(obj.Cpf);
        funcionario.SetNumCtps(obj.NumCtps);
        funcionario.SetEndereco(obj.Endereco);
        funcionario.SetEmail(obj.Email);
        funcionario.SetTelefone(obj.Telefone);
        funcionario.SetCargo(obj.Cargo);
    }
}