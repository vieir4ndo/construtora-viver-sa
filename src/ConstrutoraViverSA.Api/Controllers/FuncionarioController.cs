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

        [HttpGet("{id}")]
        public IActionResult BuscarFuncionario(long id)
        {
            var consulta = _funcionarioService.BuscarPorId(id);

            return Ok(new ApiResponse(true, new List<object> { consulta }, null));
        }

        [HttpPatch("{id}")]
        public IActionResult EditarFuncionario(FuncionarioRequest request, long id)
        {
            request.ValidarEdicao();

            _funcionarioService.Editar(id, request.RequestParaDto());

            return Ok(new ApiResponse(true, null, null));
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirFuncionario(long id)
        {
            _funcionarioService.Excluir(id);

            return Ok(new ApiResponse(true, null, null));
        }
    }
}