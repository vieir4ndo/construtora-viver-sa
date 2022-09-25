using System.Collections.Generic;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Api.Controllers.Responses;
using ConstrutoraViverSA.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConstrutoraViverSA.Api.Controllers
{
    
    [ApiController]
    [Route("obra")]
    public class ObraController : ControllerBase
    {
        private readonly IObraService _obraService;
        
        public ObraController(IObraService obraService)
        {
            _obraService = obraService;
        }
        
        [HttpPost]
        public IActionResult CadastrarObra(ObraRequest request)
        {
            request.ValidarCriacao();
            
            _obraService.Adicionar(request.RequestParaDto());

            return Ok(new ApiResponse(true, null, null));
        }
        
        [HttpGet("{obraId}")]
        public IActionResult BuscarObra(long obraId)
        {
            var consulta = _obraService.BuscarPorId(obraId);
            
            return Ok(new ApiResponse(true, new List<object> { consulta }, null));
        }
        
        [HttpPatch("{obraId}")]
        public IActionResult AlterarObra(ObraRequest request, long obraId)
        {
            request.ValidarEdicao();

            _obraService.Editar(obraId, request.RequestParaDto());

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
        public IActionResult GerenciarMaterialNaObra(GerenciarEntradaSaidaMaterialRequest materialRequest, long obraId, long materialId)
        {
            materialRequest.Validar();
            
            _obraService.GerenciarMaterial(materialRequest.RequestParaDto(),obraId, materialId);
            
            return Ok(new ApiResponse(true, null, null));
        }
    }
}
