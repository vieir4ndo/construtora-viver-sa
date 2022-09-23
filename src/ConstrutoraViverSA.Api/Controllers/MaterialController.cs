using Microsoft.AspNetCore.Mvc;
using System;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Api.Controllers.Responses;
using ConstrutoraViverSA.Service;

namespace ConstrutoraViverSA.Api.Controllers
{
    
    [ApiController]
    [Route("material")]
    public class MaterialController : ControllerBase
    {
        private readonly MaterialService _materialService;

        public MaterialController(MaterialService materialService)
        {
            _materialService = materialService;
        }
        
        [HttpPost]
        public IActionResult CadastrarMaterial(MaterialRequest request)
        {
            try
            {
                _materialService.AdicionarMaterial(request.RequestParaDto());

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponseFactory.Error(e.Message));
            }
        }
        
        [HttpGet("{id}")]
        public IActionResult BuscarMaterial(long id)
        {
            try
            {
                var consulta = _materialService.BuscarMaterialPorId(id);

                if (consulta == null)
                {
                    return NotFound(ApiResponseFactory.Error("Material não encontrado"));
                }

                return Ok(ApiResponseFactory.Success(consulta));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponseFactory.Error(e.Message));
            }
        }
        
        [HttpPatch("{id}")]
        public IActionResult EditarMaterial(MaterialRequest request, long id)
        {
            try
            {
                var consulta = _materialService.BuscarMaterialPorId(id);

                if (consulta == null)
                {
                    return NotFound(ApiResponseFactory.Error("Material não encontrado"));
                }

                _materialService.AlterarMaterial(id, request.RequestParaDto());

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponseFactory.Error(e.Message));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirMaterial(long id)
        {
            try
            {
                var consulta = _materialService.BuscarMaterialPorId(id);

                if (consulta == null)
                {
                    return NotFound(ApiResponseFactory.Error("Material não encontrado"));
                }

                _materialService.ExcluirMaterial(id);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponseFactory.Error(e.Message));
            }
        }
    }
}