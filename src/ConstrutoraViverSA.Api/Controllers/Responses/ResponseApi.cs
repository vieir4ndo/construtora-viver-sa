#nullable enable
using System.Collections.Generic;
using ConstrutoraViverSA.Domain.Exceptions;

namespace ConstrutoraViverSA.Api.Controllers.Responses;

public class ResponseApi<T> where T : class?
{
    public bool Sucesso { get; set; }
    public List<T>? Dados { get; set; }
    public string Mensagens { get; set; }

    public ResponseApi(bool? sucesso, List<T>? dados, string mensagens)
    {
        if (sucesso is null)
            throw new ResponseApiInvalido("Sucesso inválido");
        
        Sucesso = sucesso.Value;
        Dados = dados;
        Mensagens = mensagens;
    }
}