using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace ConstrutoraViverSA.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public class FuncionarioInvalidoException: Exception
{
    public FuncionarioInvalidoException(string mensagem) : base(mensagem)
    {
    }
    protected FuncionarioInvalidoException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
        serializationInfo, streamingContext)
    {
    }
}