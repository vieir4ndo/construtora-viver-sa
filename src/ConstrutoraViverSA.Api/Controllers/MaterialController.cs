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
    public ResponseApi<MaterialDto> CadastrarMaterial(MaterialRequest request)
    {
        request.ValidarCriacao();
        
        var dto = _mapper.Map<MaterialDto>(request);

        _materialService.Adicionar(dto);

        return new ResponseApi<MaterialDto>(true, null, null);
    }

    [HttpGet("{materialId}")]
    public ResponseApi<MaterialDto> BuscarMaterial(long materialId)
    {
        var consulta = _materialService.BuscarPorId(materialId);

        return new ResponseApi<MaterialDto>(true, new List<MaterialDto> { consulta }, null);
    }

    [HttpGet]
    public ResponseApi<MaterialDto> BuscarMateriais()
    {
        var consulta = _materialService.BuscarTodos();

        return new ResponseApi<MaterialDto>(true, consulta, null);
    }

    [HttpPatch("{materialId}")]
    public ResponseApi<MaterialDto> EditarMaterial(EditarMaterialRequest request, long materialId)
    {
        request.ValidarEdicao();
        
        var dto = _mapper.Map<EditarMaterialDto>(request);

        _materialService.Editar(materialId, dto);

        return new ResponseApi<MaterialDto>(true, null, null);
    }

    [HttpDelete("{materialId}")]
    public ResponseApi<MaterialDto> ExcluirMaterial(long materialId)
    {
        _materialService.Excluir(materialId);

        return new ResponseApi<MaterialDto>(true, null, null);
    }

    [HttpPut("{materialId}/estoque")]
    public ResponseApi<MaterialDto> GerenciarEstoque(EntradaSaidaMaterialRequest request,
        long materialId)
    {
        request.Validar();

        _materialService.MovimentarEstoque(materialId, _mapper.Map<EntradaSaidaMaterialDto>(request));

        return new ResponseApi<MaterialDto>(true, null, null);
    }
}