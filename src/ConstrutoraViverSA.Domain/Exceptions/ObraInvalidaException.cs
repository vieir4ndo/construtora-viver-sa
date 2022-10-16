using System;
using System.Diagnostics.CodeAnalysis;

namespace ConstrutoraViverSA.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public class ObraInvalidaException : Exception
{
    public ObraInvalidaException(string mensagem) : base(mensagem)
    {
        
    }
}