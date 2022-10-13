using System;

namespace ConstrutoraViverSA.Domain.Exceptions;

public class ObraMaterialInvalidaException : Exception
{
    public ObraMaterialInvalidaException(string mensagem) : base(mensagem)
    {
        
    }
}