using System.Collections.Generic;
using AutoMapper;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Api.Controllers.Responses;
using ConstrutoraViverSA.Application.Interfaces;
using ConstrutoraViverSA.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ConstrutoraViverSA.Api.Controllers;

[ApiController]
[Route("funcionario")]
public class FuncionarioController : ControllerBase
{
    private readonly IFuncionarioService _funcionarioService;
    private readonly IMapper _mapper;

    public FuncionarioController(IFuncionarioService funcionarioService, IMapper mapper)
    {
        _funcionarioService = funcionarioService;
        _mapper = mapper;
    }

    [HttpPost]
    public ResponseApi<FuncionarioDto> CadastrarFuncionario(FuncionarioRequest request)
    {
        request.ValidarCriacao();

        var dto = _mapper.Map<FuncionarioDto>(request);
        
        _funcionarioService.Adicionar(dto);

        return new ResponseApi<FuncionarioDto>(true, null, null);
    }

    [HttpGet("{funcionarioId}")]
    public ResponseApi<FuncionarioDto> BuscarFuncionario(long funcionarioId)
    {
        var consulta = _funcionarioService.BuscarPorId(funcionarioId);

        return new ResponseApi<FuncionarioDto>(true, new List<FuncionarioDto>() {consulta}, null);
    }
    
    [HttpGet]
    public ResponseApi<FuncionarioDto> BuscarFuncionarios()
    {
        var consulta = _funcionarioService.BuscarTodos();

        return new ResponseApi<FuncionarioDto>(true, consulta, null);
    }

    [HttpPatch("{funcionarioId}")]
    public ResponseApi<FuncionarioDto> EditarFuncionario(FuncionarioRequest request, long funcionarioId)
    {
        request.ValidarEdicao();
        
        var dto = _mapper.Map<FuncionarioDto>(request);

        _funcionarioService.Editar(funcionarioId, dto);

        return new ResponseApi<FuncionarioDto>(true, null, null);
    }

    [HttpDelete("{funcionarioId}")]
    public ResponseApi<FuncionarioDto> ExcluirFuncionario(long funcionarioId)
    {
        _funcionarioService.Excluir(funcionarioId);

        return new ResponseApi<FuncionarioDto>(true, null, null);
    }
}