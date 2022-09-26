using System.Collections.Generic;
using ConstrutoraViverSA.Application.Interfaces;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Exceptions;
using ConstrutoraViverSA.Repository.Interfaces;

namespace ConstrutoraViverSA.Application.Services;

public class FuncionarioService : IFuncionarioService
{
    private readonly IFuncionarioRepository _repository;

    public FuncionarioService(IFuncionarioRepository repository)
    {
        _repository = repository;
    }

    public List<Funcionario> BuscarTodos()
    {
        return _repository.BuscarTodos();
    }

    public Funcionario BuscarPorId(long buscaId)
    {
        var funcionario = _repository.BuscarPorId(buscaId);

        if (funcionario is null) throw new NaoEncontradoException("Funcionário não encontrado");

        return funcionario;
    }

    public void Adicionar(FuncionarioDto dto)
    {
        // TODO: Usar automapper
        _repository.Adicionar(dto.DtoParaDominio());
    }

    public void Excluir(long idExcluir)
    {
        var funcionario = BuscarPorId(idExcluir);

        _repository.Excluir(funcionario);
    }

    public void Editar(long id, FuncionarioDto dto)
    {
        var funcionario = BuscarPorId(id);

        funcionario.Nome = dto.Nome ?? funcionario.Nome;
        funcionario.DataNascimento = dto.DataNascimento ?? funcionario.DataNascimento;
        funcionario.Genero = dto.Genero ?? funcionario.Genero;
        funcionario.Cpf = dto.Cpf ?? funcionario.Cpf;
        funcionario.NumCtps = dto.NumCtps ?? funcionario.NumCtps;
        funcionario.Endereco = dto.Endereco ?? funcionario.Endereco;
        funcionario.Email = dto.Email ?? funcionario.Email;
        funcionario.Telefone = dto.Telefone ?? funcionario.Telefone;
        funcionario.Cargo = dto.Cargo ?? funcionario.Cargo;

        _repository.Editar(funcionario);
    }
}