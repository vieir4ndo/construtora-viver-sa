using System;
using System.Diagnostics.CodeAnalysis;

namespace ConstrutoraViverSA.Domain.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class MaterialInvalidoException : Exception
{
    public MaterialInvalidoException(string mensagem) : base(mensagem)
    {
        
    }
}