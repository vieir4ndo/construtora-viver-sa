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
    public IActionResult CadastrarFuncionario(FuncionarioRequest request)
    {
        request.ValidarCriacao();

        var dto = _mapper.Map<FuncionarioDto>(request);
        
        _funcionarioService.Adicionar(dto);

        return Ok(new ResponseApi(true, null, null));
    }

    [HttpGet("{funcionarioId}")]
    public IActionResult BuscarFuncionario(long funcionarioId)
    {
        var consulta = _funcionarioService.BuscarPorId(funcionarioId);

        return Ok(new ResponseApi(true, new List<object> { consulta }, null));
    }
    
    [HttpGet]
    public IActionResult BuscarFuncionarios()
    {
        var consulta = _funcionarioService.BuscarTodos();

        return Ok(new ResponseApi(true, new List<object> { consulta }, null));
    }

    [HttpPatch("{funcionarioId}")]
    public IActionResult EditarFuncionario(FuncionarioRequest request, long funcionarioId)
    {
        request.ValidarEdicao();
        
        var dto = _mapper.Map<FuncionarioDto>(request);

        _funcionarioService.Editar(funcionarioId, dto);

        return Ok(new ResponseApi(true, null, null));
    }

    [HttpDelete("{funcionarioId}")]
    public IActionResult ExcluirFuncionario(long funcionarioId)
    {
        _funcionarioService.Excluir(funcionarioId);

        return Ok(new ResponseApi(true, null, null));
    }
}