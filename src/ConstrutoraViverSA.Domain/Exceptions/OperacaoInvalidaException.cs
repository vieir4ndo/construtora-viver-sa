using System;
using System.Diagnostics.CodeAnalysis;

namespace ConstrutoraViverSA.Domain.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class OperacaoInvalidaException : Exception
{
    public OperacaoInvalidaException(string mensagem) : base(mensagem)
    {
    }
}