using System.Collections.Generic;
using System.Linq;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Exceptions;
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
        
        if (funcionario is null)
            throw new NaoEncontradoException("Funcionário não encontrado.");

        return funcionario;
    }

    public void Adicionar(Funcionario obj)
    {
        Funcionarios.Add(obj);
    }

    public void Excluir(Funcionario obj)
    {
        var funcionario = Funcionarios.FirstOrDefault(x => x.Id == obj.Id);
        
        if (funcionario is null)
            throw new NaoEncontradoException("Funcionário não encontrado.");

        Funcionarios.Remove(funcionario);
    }

    public void Editar(Funcionario obj)
    {
        var funcionario = Funcionarios.FirstOrDefault(x => x.Id == obj.Id);

        if (funcionario is null)
            throw new NaoEncontradoException("Funcionário não encontrado.");
        
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