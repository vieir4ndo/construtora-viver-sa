using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace ConstrutoraViverSA.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public class ObraMaterialInvalidaException : Exception
{
    public ObraMaterialInvalidaException(string mensagem) : base(mensagem)
    {
    }
    
    protected ObraMaterialInvalidaException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
        serializationInfo, streamingContext)
    {
    }
}