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

        return Ok(new ResponseApi(true, null, null));
    }

    [HttpGet("{orcamentoId}")]
    public IActionResult BuscarOrcamento(long orcamentoId)
    {
        var consulta = _orcamentoService.BuscarPorId(orcamentoId);

        return Ok(new ResponseApi(true, new List<object>() { consulta }, null));
    }

    [HttpGet]
    public IActionResult BuscarOrcamentos()
    {
        var consulta = _orcamentoService.BuscarTodos();

        return Ok(new ResponseApi(true, new List<object>() { consulta }, null));
    }

    [HttpPatch("{orcamentoId}")]
    public IActionResult EditarOrcamento(OrcamentoRequest request, long orcamentoId)
    {
        request.ValidarEdicao();

        var dto = _mapper.Map<OrcamentoDto>(request);
        
        _orcamentoService.Editar(orcamentoId, dto);

        return Ok(new ResponseApi(true, null, null));
    }

    [HttpDelete("{orcamentoId}")]
    public IActionResult ExcluirOrcamento(long orcamentoId)
    {
        _orcamentoService.Excluir(orcamentoId);

        return Ok(new ResponseApi(true, null, null));
    }
}