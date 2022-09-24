using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Api.Controllers.Responses;
using ConstrutoraViverSA.Application.Interfaces;

namespace ConstrutoraViverSA.Api.Controllers
{
    
    [ApiController]
    [Route("material")]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }
        
        [HttpPost]
        public IActionResult CadastrarMaterial(MaterialRequest request)
        {
            try
            {
                _materialService.AdicionarMaterial(request.RequestParaDto());

                return Ok(new ApiResponse(true, null, null));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse(false, null, new List<string>(){ e.Message}));
            }
        }
        
        [HttpGet("{id}")]
        public IActionResult BuscarMaterial(long id)
        {
            try
            {
                var consulta = _materialService.BuscarMaterialPorId(id);

                return Ok(new ApiResponse(true, new List<object>() { consulta}, null));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse(false, null, new List<string>(){ e.Message}));
            }
        }
        
        [HttpPatch("{id}")]
        public IActionResult EditarMaterial(MaterialRequest request, long id)
        {
            try
            {
                _materialService.AlterarMaterial(id, request.RequestParaDto());

                return Ok(new ApiResponse(true, null, null));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse(false, null, new List<string>(){ e.Message}));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirMaterial(long id)
        {
            try
            {
                _materialService.ExcluirMaterial(id);

                return Ok(new ApiResponse(true, null, null));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse(false, null, new List<string>(){ e.Message}));
            }
        }
    }
}