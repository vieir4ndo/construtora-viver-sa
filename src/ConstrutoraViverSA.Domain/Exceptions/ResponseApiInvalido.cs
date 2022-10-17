using System;

namespace ConstrutoraViverSA.Domain.Exceptions;

public class ResponseApiInvalido : Exception
{
    public ResponseApiInvalido(string mensagem) : base(mensagem)
    {
        
    }
}