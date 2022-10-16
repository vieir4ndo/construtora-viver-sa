using System;
using System.Diagnostics.CodeAnalysis;

namespace ConstrutoraViverSA.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public class ErroValidacaoException : Exception
{
    public ErroValidacaoException(string message) : base(message)
    {
    }
}