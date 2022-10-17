using System.Collections.Generic;
using AutoMapper;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Api.Controllers.Responses;
using ConstrutoraViverSA.Application.Interfaces;
using ConstrutoraViverSA.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ConstrutoraViverSA.Api.Controllers;

[ApiController]
[Route("obra")]
public class ObraController : ControllerBase
{
    private readonly IObraService _obraService;
    private readonly IMapper _mapper;

    public ObraController(IObraService obraService, IMapper mapper)
    {
        _obraService = obraService;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult CadastrarObra(ObraRequest request)
    {
        request.ValidarCriacao();

        var dto = _mapper.Map<ObraDto>(request);

        _obraService.Adicionar(dto);

        return Ok(new ApiResponse(true, null, null));
    }

    [HttpGet("{obraId}")]
    public IActionResult BuscarObra(long obraId)
    {
        var consulta = _obraService.BuscarPorId(obraId);

        return Ok(new ApiResponse(true, new List<object> { consulta }, null));
    }
    
    [HttpGet]
    public IActionResult BuscarObras()
    {
        var consulta = _obraService.BuscarTodos();

        return Ok(new ApiResponse(true, new List<object> { consulta }, null));
    }

    [HttpPatch("{obraId}")]
    public IActionResult AlterarObra(ObraRequest request, long obraId)
    {
        request.ValidarEdicao();
        
        var dto = _mapper.Map<ObraDto>(request);

        _obraService.Editar(obraId, dto);

        return Ok(new ApiResponse(true, null, null));
    }

    [HttpDelete("{obraId}")]
    public IActionResult ExcluirObra(long obraId)
    {
        _obraService.Excluir(obraId);

        return Ok(new ApiResponse(true, null, null));
    }

    [HttpPost("{obraId}/funcionario/{funcionarioId}")]
    public IActionResult AlocarFuncionarioNaObra(long obraId, long funcionarioId)
    {
        _obraService.AlocarFuncionario(obraId, funcionarioId);

        return Ok(new ApiResponse(true, null, null));
    }

    [HttpDelete("{obraId}/funcionario/{funcionarioId}")]
    public IActionResult DesalocarFuncionarioNaObra(long obraId, long funcionarioId)
    {
        _obraService.DesalocarFuncionario(obraId, funcionarioId);

        return Ok(new ApiResponse(true, null, null));
    }

    [HttpPut("{obraId}/material/{materialId}")]
    public IActionResult GerenciarMaterialNaObra(EntradaSaidaMaterialRequest request, long obraId,
        long materialId)
    {
        request.Validar();

        _obraService.GerenciarMaterial(_mapper.Map<EntradaSaidaMaterialDto>(request), obraId, materialId);

        return Ok(new ApiResponse(true, null, null));
    }
}