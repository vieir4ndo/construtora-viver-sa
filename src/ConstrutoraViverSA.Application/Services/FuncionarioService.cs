using System.Collections.Generic;
using AutoMapper;
using ConstrutoraViverSA.Application.Interfaces;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Exceptions;
using ConstrutoraViverSA.Repository.Interfaces;

namespace ConstrutoraViverSA.Application.Services;

public class FuncionarioService : IFuncionarioService
{
    private readonly IFuncionarioRepository _repository;
    private readonly IMapper _mapper;

    public FuncionarioService(IFuncionarioRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public List<FuncionarioDto> BuscarTodos()
    {
        var funcionarios = _repository.BuscarTodos();

        var listaFuncionariosDto = new List<FuncionarioDto>();
            
        funcionarios.ForEach(x => listaFuncionariosDto.Add(_mapper.Map<FuncionarioDto>(x)));

        return listaFuncionariosDto;
    }

    public Funcionario BuscarEntidadePorId(long buscaId)
    {
        var funcionario = _repository.BuscarPorId(buscaId);

        if (funcionario is null) throw new NaoEncontradoException("Funcionário não encontrado");

        return funcionario;
    }

    public FuncionarioDto BuscarPorId(long buscaId)
    {
        var funcionario = BuscarEntidadePorId(buscaId);
        
        return _mapper.Map<FuncionarioDto>(funcionario);
    }

    public void Adicionar(FuncionarioDto dto)
    {
        var funcionario = _mapper.Map<Funcionario>(dto);
        _repository.Adicionar(funcionario);
    }

    public void Excluir(long idExcluir)
    {
        var funcionario = BuscarEntidadePorId(idExcluir);

        _repository.Excluir(funcionario);
    }

    public void Editar(long id, FuncionarioDto dto)
    {
        var funcionario = BuscarEntidadePorId(id);

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