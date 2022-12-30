using System;
using System.Diagnostics.CodeAnalysis;

namespace ConstrutoraViverSA.Domain.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class ObraMaterialInvalidaException : Exception
{
    public ObraMaterialInvalidaException(string mensagem) : base(mensagem)
    {
        
    }
}