using System.Collections.Generic;

namespace ConstrutoraViverSA.Api.Controllers.Responses
{
    public class ApiResponse
    {
        public bool Sucesso { get; set; }
        public List<object> Dados { get; set; }
        public List<string> Mensagens { get; set; }

        public ApiResponse(bool sucesso, List<object> dados, List<string> mensagens)
        {
            Sucesso = sucesso;
            Dados = dados;
            Mensagens = mensagens;
        }
    }
}