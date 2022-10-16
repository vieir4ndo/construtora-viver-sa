using System;
using System.Diagnostics.CodeAnalysis;

namespace ConstrutoraViverSA.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public class OrcamentoInvalidoException : Exception
{
    public OrcamentoInvalidoException(string mensagem) : base(mensagem)
    {
        
    }
}