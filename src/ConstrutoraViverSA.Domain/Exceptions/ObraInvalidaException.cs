using System;

namespace ConstrutoraViverSA.Domain.Exceptions;

public class ObraInvalidaException : Exception
{
    public ObraInvalidaException(string mensagem) : base(mensagem)
    {
        
    }
}