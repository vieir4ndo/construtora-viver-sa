using System.Collections.Generic;
using AutoMapper;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Api.Controllers.Responses;
using ConstrutoraViverSA.Application.Interfaces;
using ConstrutoraViverSA.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ConstrutoraViverSA.Api.Controllers;

[ApiController]
[Route("orcamento")]
public class OrcamentoController : ControllerBase
{
    private readonly IOrcamentoService _orcamentoService;
    private readonly IMapper _mapper;

    public OrcamentoController(IOrcamentoService orcamentoService, IMapper mapper)
    {
        _orcamentoService = orcamentoService;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult CadastrarOrcamento(OrcamentoRequest request)
    {
        request.ValidarCriacao();

        var dto = _mapper.Map<OrcamentoDto>(request);

        _orcamentoService.Adicionar(dto);

        return Ok(new ApiResponse(true, null, null));
    }

    [HttpGet("{id}")]
    public IActionResult BuscarOrcamento(long id)
    {
        var consulta = _orcamentoService.BuscarPorId(id);

        return Ok(new ApiResponse(true, new List<object>() { consulta }, null));
    }

    [HttpPatch("{id}")]
    public IActionResult EditarOrcamento(OrcamentoRequest request, long id)
    {
        request.ValidarEdicao();

        var dto = _mapper.Map<OrcamentoDto>(request);
        
        _orcamentoService.Editar(id, dto);

        return Ok(new ApiResponse(true, null, null));
    }

    [HttpDelete("{id}")]
    public IActionResult ExcluirOrcamento(long id)
    {
        _orcamentoService.Excluir(id);

        return Ok(new ApiResponse(true, null, null));
    }
}