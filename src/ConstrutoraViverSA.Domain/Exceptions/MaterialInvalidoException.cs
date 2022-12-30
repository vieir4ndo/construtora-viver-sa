using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace ConstrutoraViverSA.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public class MaterialInvalidoException : Exception
{
    public MaterialInvalidoException(string mensagem) : base(mensagem)
    {
    }
    protected MaterialInvalidoException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
        serializationInfo, streamingContext)
    {
    }
}