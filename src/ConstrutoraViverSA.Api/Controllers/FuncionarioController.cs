using System.Collections.Generic;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Api.Controllers.Responses;
using ConstrutoraViverSA.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            request.ValidarCriacao();

            _funcionarioService.Adicionar(request.RequestParaDto());

            return Ok(new ApiResponse(true, null, null));
        }

        [HttpGet("{funcionarioId}")]
        public IActionResult BuscarFuncionario(long funcionarioId)
        {
            var consulta = _funcionarioService.BuscarPorId(funcionarioId);

            return Ok(new ApiResponse(true, new List<object> { consulta }, null));
        }

        [HttpPatch("{funcionarioId}")]
        public IActionResult EditarFuncionario(FuncionarioRequest request, long funcionarioId)
        {
            request.ValidarEdicao();

            _funcionarioService.Editar(funcionarioId, request.RequestParaDto());

            return Ok(new ApiResponse(true, null, null));
        }

        [HttpDelete("{funcionarioId}")]
        public IActionResult ExcluirFuncionario(long funcionarioId)
        {
            _funcionarioService.Excluir(funcionarioId);

            return Ok(new ApiResponse(true, null, null));
        }
    }
}