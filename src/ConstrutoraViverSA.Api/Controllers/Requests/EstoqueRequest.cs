using ConstrutoraViverSA.Api.Controllers.Validators;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;

namespace ConstrutoraViverSA.Api.Controllers.Requests
{
    public class EstoqueRequest
    {
        public OperacaoEstoque OperacaoEstoque { get; set; }
        public int Quantidade { get; set; }

        public EstoqueDto RequestParaDto()
        {
            return new EstoqueDto
            {
                OperacaoEstoque = OperacaoEstoque,
                Quantidade = Quantidade
            };
        }

        public void Validar()
        {
            var resultado = new EstoqueValidator().Validate(this);

            if (resultado.IsValid == false) throw new ErroValidacaoException(resultado.ToString());
        }
    }
}