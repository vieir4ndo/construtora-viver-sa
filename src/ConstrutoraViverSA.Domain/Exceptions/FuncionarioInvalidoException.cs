using System;

namespace ConstrutoraViverSA.Domain.Exceptions;

public class FuncionarioInvalidoException: Exception
{
    public FuncionarioInvalidoException(string mensagem) : base(mensagem)
    {
        
    }
}