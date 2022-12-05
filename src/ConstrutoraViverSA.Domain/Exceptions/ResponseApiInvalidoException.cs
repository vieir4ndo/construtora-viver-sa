using System;
using System.Diagnostics.CodeAnalysis;

namespace ConstrutoraViverSA.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public class ResponseApiInvalidoException : Exception
{
    public ResponseApiInvalidoException(string mensagem) : base(mensagem)
    {
        
    }
}