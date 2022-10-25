using System;
using System.Diagnostics.CodeAnalysis;

namespace ConstrutoraViverSA.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public class ResponseApiInvalido : Exception
{
    public ResponseApiInvalido(string mensagem) : base(mensagem)
    {
        
    }
}