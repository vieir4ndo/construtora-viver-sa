using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace ConstrutoraViverSA.Api.Controllers.Responses
{
    public class ApiResponse
    {
        private bool Sucesso { get; set; }
        private List<object> Dados { get; set; }
        private List<string> Mensagens { get; set; }

        public ApiResponse(bool sucesso, List<object> dados, List<string> mensagens)
        {
            Sucesso = sucesso;
            Dados = dados;
            Mensagens = mensagens;
        }
    }
}