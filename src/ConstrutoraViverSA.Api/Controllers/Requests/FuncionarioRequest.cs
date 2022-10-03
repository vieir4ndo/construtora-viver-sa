using System;
using ConstrutoraViverSA.Api.Controllers.Validators;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;

namespace ConstrutoraViverSA.Api.Controllers.Requests;

public class FuncionarioRequest
{
    public string Nome { get; set; }
    public DateTime? DataNascimento { get; set; }
    public GeneroEnum? Genero { get; set; }
    public string Cpf { get; set; }
    public string NumCtps { get; set; }
    public string Endereco { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public CargoEnum? Cargo { get; set; }

    public void ValidarCriacao()
    {
        var resultado = new CriarFuncionarioValidator().Validate(this);

        if (resultado.IsValid == false)
        {
            throw new ErroValidacaoException(resultado.ToString());
        }
    }

    public void ValidarEdicao()
    {
        var resultado = new EditarFuncionarioValidator().Validate(this);

        if (resultado.IsValid == false)
        {
            throw new ErroValidacaoException(resultado.ToString());
        }
    }
}