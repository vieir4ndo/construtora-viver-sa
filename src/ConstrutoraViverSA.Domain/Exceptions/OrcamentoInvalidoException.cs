using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace ConstrutoraViverSA.Domain.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class OrcamentoInvalidoException : Exception
{
    public OrcamentoInvalidoException(string mensagem) : base(mensagem)
    {
        
    }
    
    protected OrcamentoInvalidoException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
        serializationInfo, streamingContext)
    {
        
    }
}