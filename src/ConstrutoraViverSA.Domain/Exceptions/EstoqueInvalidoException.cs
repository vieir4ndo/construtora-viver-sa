using System;
using System.Diagnostics.CodeAnalysis;

namespace ConstrutoraViverSA.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public class EstoqueInvalidoException : Exception
{
    public EstoqueInvalidoException(string mensagem) : base(mensagem)
    {
        
    }
}