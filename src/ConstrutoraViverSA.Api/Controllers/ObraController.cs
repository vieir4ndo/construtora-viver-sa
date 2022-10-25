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
    public ResponseApi<ObraDto> CadastrarObra(ObraRequest request)
    {
        request.ValidarCriacao();

        var dto = _mapper.Map<ObraDto>(request);

        _obraService.Adicionar(dto);

        return new ResponseApi<ObraDto>(true, null, null);
    }

    [HttpGet("{obraId}")]
    public ResponseApi<ObraDto> BuscarObra(long obraId)
    {
        var consulta = _obraService.BuscarPorId(obraId);

        return new ResponseApi<ObraDto>(true, new List<ObraDto> { consulta }, null);
    }
    
    [HttpGet]
    public ResponseApi<ObraDto> BuscarObras()
    {
        var consulta = _obraService.BuscarTodos();

        return new ResponseApi<ObraDto>(true, consulta, null);
    }

    [HttpPatch("{obraId}")]
    public ResponseApi<ObraDto> EditarObra(EditarObraRequest request, long obraId)
    {
        request.ValidarEdicao();
        
        var dto = _mapper.Map<ObraDto>(request);

        _obraService.Editar(obraId, dto);

        return new ResponseApi<ObraDto>(true, null, null);
    }

    [HttpDelete("{obraId}")]
    public ResponseApi<ObraDto> ExcluirObra(long obraId)
    {
        _obraService.Excluir(obraId);

        return new ResponseApi<ObraDto>(true, null, null);
    }

    [HttpPost("{obraId}/funcionario/{funcionarioId}")]
    public ResponseApi<ObraDto> AlocarFuncionarioNaObra(long obraId, long funcionarioId)
    {
        _obraService.AlocarFuncionario(obraId, funcionarioId);

        return new ResponseApi<ObraDto>(true, null, null);
    }

    [HttpDelete("{obraId}/funcionario/{funcionarioId}")]
    public ResponseApi<ObraDto> DesalocarFuncionarioNaObra(long obraId, long funcionarioId)
    {
        _obraService.DesalocarFuncionario(obraId, funcionarioId);

        return new ResponseApi<ObraDto>(true, null, null);
    }

    [HttpPut("{obraId}/material/{materialId}")]
    public ResponseApi<ObraDto> GerenciarMaterialNaObra(EntradaSaidaMaterialRequest request, long obraId,
        long materialId)
    {
        request.Validar();

        var dto = _mapper.Map<EntradaSaidaMaterialDto>(request);
        
        _obraService.GerenciarMaterial(dto, obraId, materialId);

        return new ResponseApi<ObraDto>(true, null, null);
    }
}