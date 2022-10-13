using System;

namespace ConstrutoraViverSA.Domain.Exceptions;

public class EstoqueInvalidoException : Exception
{
    public EstoqueInvalidoException(string mensagem) : base(mensagem)
    {
        
    }
}