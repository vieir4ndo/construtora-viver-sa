using System;
using System.Diagnostics.CodeAnalysis;

namespace ConstrutoraViverSA.Domain.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class ErroValidacaoException : Exception
{
    public ErroValidacaoException(string message) : base(message)
    {
    }
}