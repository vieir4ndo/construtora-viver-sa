using System;
using System.Collections.Generic;

namespace ConstrutoraViverSA.Domain.Exceptions
{
    public class ErroValidacaoException<T> : Exception
    {
        public List<T> Erros { get; set; }
        public ErroValidacaoException(List<T> erros)
        {
            Erros = erros;
        }
    }
}