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
        
        [HttpGet("{id}")]
        public IActionResult BuscarObra(long id)
        {
            var consulta = _obraService.BuscarPorId(id);
            
            return Ok(new ApiResponse(true, new List<object> { consulta }, null));
        }
        
        [HttpPatch("{id}")]
        public IActionResult AlterarObra(ObraRequest request, long id)
        {
            request.ValidarEdicao();

            _obraService.Editar(id, request.RequestParaDto());

            return Ok(new ApiResponse(true, null, null));
        }
  
        [HttpDelete("{id}")]
        public IActionResult ExcluirObra(long id)
        {
            _obraService.Excluir(id);
            
            return Ok(new ApiResponse(true, null, null));
        }

        [HttpPost("{id}/funcionario/{funcionarioId}")]
        public IActionResult AlocarFuncionarioNaObra(long id, long funcionarioId)
        {
            _obraService.AlocarFuncionario(id, funcionarioId);
            
            return Ok(new ApiResponse(true, null, null));
        }
        
        [HttpDelete("{id}/funcionario/{funcionarioId}")]
        public IActionResult DesalocarFuncionarioDaObra(long id, long funcionarioId)
        {
            _obraService.DesalocarFuncionario(id, funcionarioId);
            
            return Ok(new ApiResponse(true, null, null));
        }
    }
}
