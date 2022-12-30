using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace ConstrutoraViverSA.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public class ResponseApiInvalidoException : Exception
{
    public ResponseApiInvalidoException(string mensagem) : base(mensagem)
    {
    }
    
    protected ResponseApiInvalidoException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
        serializationInfo, streamingContext)
    {
    }
}