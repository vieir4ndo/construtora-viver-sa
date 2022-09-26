using System;

namespace ConstrutoraViverSA.Domain.Exceptions;

public class ErroValidacaoException : Exception
{
    public ErroValidacaoException(string message) : base(message)
    {
    }
}