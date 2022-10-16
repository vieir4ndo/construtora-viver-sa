using System;
using System.Diagnostics.CodeAnalysis;

namespace ConstrutoraViverSA.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public class ObraMaterialInvalidaException : Exception
{
    public ObraMaterialInvalidaException(string mensagem) : base(mensagem)
    {
        
    }
}