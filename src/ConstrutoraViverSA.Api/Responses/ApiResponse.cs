using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace ConstrutoraViverSA.Api.Responses
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

    public static class ApiResponseFactory
    {
        public static ApiResponse Error(string mensagem)
        {
            return new ApiResponse(false, null, new List<string>()
            {
                mensagem
            });
        }
        
        public static ApiResponse Error(List<string> mensagem)
        {
            return new ApiResponse(false, null, mensagem);
        }
        
        public static ApiResponse Success(List<object> data = null)
        {
            return new ApiResponse(true, data, null);
        }
    }
}