using System;

namespace ConstrutoraViverSA.Domain.Exceptions;

public class OrcamentoInvalidoException : Exception
{
    public OrcamentoInvalidoException(string mensagem) : base(mensagem)
    {
        
    }
}