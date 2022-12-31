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
    public ResponseApi<OrcamentoDto> CadastrarOrcamento(OrcamentoRequest request)
    {
        request.ValidarCriacao();

        var dto = _mapper.Map<OrcamentoDto>(request);

        _orcamentoService.Adicionar(dto);

        return new ResponseApi<OrcamentoDto>(true, null, null);
    }

    [HttpGet("{orcamentoId}")]
    public ResponseApi<OrcamentoDto> BuscarOrcamento(long orcamentoId)
    {
        var consulta = _orcamentoService.BuscarPorId(orcamentoId);

        return new ResponseApi<OrcamentoDto>(true, new List<OrcamentoDto>() { consulta }, null);
    }

    [HttpGet]
    public ResponseApi<OrcamentoDto> BuscarOrcamentos()
    {
        var consulta = _orcamentoService.BuscarTodos();

        return new ResponseApi<OrcamentoDto>(true,  consulta, null);
    }

    [HttpPatch("{orcamentoId}")]
    public ResponseApi<OrcamentoDto> EditarOrcamento(OrcamentoRequest request, long orcamentoId)
    {
        request.ValidarEdicao();

        var dto = _mapper.Map<OrcamentoDto>(request);
        
        _orcamentoService.Editar(orcamentoId, dto);

        return new ResponseApi<OrcamentoDto>(true, null, null);
    }

    [HttpDelete("{orcamentoId}")]
    public ResponseApi<OrcamentoDto> ExcluirOrcamento(long orcamentoId)
    {
        _orcamentoService.Excluir(orcamentoId);

        return new ResponseApi<OrcamentoDto>(true, null, null);
    }
}