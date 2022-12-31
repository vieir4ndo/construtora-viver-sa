using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace ConstrutoraViverSA.Domain.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class EstoqueInvalidoException : Exception
{
    public EstoqueInvalidoException(string mensagem) : base(mensagem)
    {
    }
    protected EstoqueInvalidoException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
        serializationInfo, streamingContext)
    {
    }
}