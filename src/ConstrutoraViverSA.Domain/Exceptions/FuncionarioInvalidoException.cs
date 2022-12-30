using System;
using System.Diagnostics.CodeAnalysis;

namespace ConstrutoraViverSA.Domain.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class FuncionarioInvalidoException: Exception
{
    public FuncionarioInvalidoException(string mensagem) : base(mensagem)
    {
        
    }
}