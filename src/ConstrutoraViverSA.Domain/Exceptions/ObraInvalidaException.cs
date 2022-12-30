using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace ConstrutoraViverSA.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public class ObraInvalidaException : Exception
{
    public ObraInvalidaException(string mensagem) : base(mensagem)
    {
    }
    protected ObraInvalidaException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
        serializationInfo, streamingContext)
    {
    }
}