using System.Collections.Generic;
using AutoMapper;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Api.Controllers.Responses;
using ConstrutoraViverSA.Application.Interfaces;
using ConstrutoraViverSA.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ConstrutoraViverSA.Api.Controllers;

[ApiController]
[Route("material")]
public class MaterialController : ControllerBase
{
    private readonly IMaterialService _materialService;
    private readonly IMapper _mapper;

    public MaterialController(IMaterialService materialService, IMapper mapper)
    {
        _materialService = materialService;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult CadastrarMaterial(MaterialRequest request)
    {
        request.ValidarCriacao();
        
        var dto = _mapper.Map<MaterialDto>(request);

        _materialService.Adicionar(dto);

        return Ok(new ApiResponse(true, null, null));
    }

    [HttpGet("{materialId}")]
    public IActionResult BuscarMaterial(long materialId)
    {
        var consulta = _materialService.BuscarPorId(materialId);

        return Ok(new ApiResponse(true, new List<object> { consulta }, null));
    }

    [HttpPatch("{materialId}")]
    public IActionResult EditarMaterial(MaterialRequest request, long materialId)
    {
        request.ValidarEdicao();
        
        var dto = _mapper.Map<MaterialDto>(request);

        _materialService.Editar(materialId, dto);

        return Ok(new ApiResponse(true, null, null));
    }

    [HttpDelete("{materialId}")]
    public IActionResult ExcluirMaterial(long materialId)
    {
        _materialService.Excluir(materialId);

        return Ok(new ApiResponse(true, null, null));
    }

    [HttpPut("{materialId}/estoque")]
    public IActionResult GerenciarEstoque(GerenciarEntradaSaidaMaterialRequest materiaisMaterialRequest,
        long materialId)
    {
        materiaisMaterialRequest.Validar();

        _materialService.MovimentarEstoque(materialId, materiaisMaterialRequest.RequestParaDto());

        return Ok(new ApiResponse(true, null, null));
    }
}