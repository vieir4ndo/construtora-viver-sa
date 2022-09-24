using Microsoft.AspNetCore.Mvc;
using System;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Api.Controllers.Responses;
using ConstrutoraViverSA.Application.Interfaces;

namespace ConstrutoraViverSA.Api.Controllers
{
    
    [ApiController]
    [Route("funcionario")]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;

        public FuncionarioController(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        [HttpPost]
        public IActionResult CadastrarFuncionario(FuncionarioRequest request)
        {
            try
            {
                // TODO: Validar request
                // TODO: Usar Automapper
                
                _funcionarioService.AdicionarFuncionario(request.RequestParaDto());
                
                return Ok(ApiResponseFactory.Success());
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponseFactory.Error(e.Message));
            }
        }
        
        [HttpGet("{id}")]
        public IActionResult BuscarFuncionario(long id)
        {
            try
            {
                // TODO: Levar lógica para service
                var consulta = _funcionarioService.BuscarFuncionarioPorId(id);

                if (consulta == null)
                {
                    return NotFound(ApiResponseFactory.Error("Funcionário não encontrado"));
                }
                
                // TODO: Mapear para objeto de response

                return Ok(consulta);
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponseFactory.Error(e.Message));
            }
        }

        [HttpPatch("{id}")]
        public IActionResult EditarFuncionario(FuncionarioRequest request, long id)
        {
            try
            {
                var consulta = _funcionarioService.BuscarFuncionarioPorId(id);

                if (consulta == null)
                {
                    return NotFound(ApiResponseFactory.Error("Funcionário não encontrado"));
                }

                _funcionarioService.AlterarFuncionario(id, request.RequestParaDto());

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponseFactory.Error(e.Message));
            }
        }
        
        [HttpDelete("{id}")]
        public IActionResult ExcluirFuncionario(long id)
        {
            try
            {
                var consulta = _funcionarioService.BuscarFuncionarioPorId(id);

                if (consulta == null)
                {
                    return NotFound(ApiResponseFactory.Error("Funcionário não encontrado"));
                }

                _funcionarioService.ExcluirFuncionario(id);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponseFactory.Error(e.Message));
            }
        }
    }
}
