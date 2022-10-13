using System;

namespace ConstrutoraViverSA.Domain.Exceptions;

public class MaterialInvalidoException : Exception
{
    public MaterialInvalidoException(string mensagem) : base(mensagem)
    {
        
    }
}