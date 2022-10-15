using ConstrutoraViverSA.Domain.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConstrutoraViverSA.Domain.Exceptions;

namespace ConstrutoraViverSA.Domain;

public sealed class Material
{
    public long Id { get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public TipoMaterialEnum Tipo { get; private set; }
    public double Valor { get; private set; }
    public int Quantidade { get; private set; }
    public ICollection<Estoque> Estoque { get; private set; }
    public ICollection<ObraMaterial> ObraMateriais { get; private set; }

    public Material()
    {
    }

    public Material(string nome, string descricao, TipoMaterialEnum? tipo, double? valor, int? quantidade)
    {
        var erros = new StringBuilder();

        if (string.IsNullOrWhiteSpace(nome))
            erros.Append("Nome inválido.");

        if (string.IsNullOrWhiteSpace(descricao))
            erros.Append("Descrição inválida.");

        if (tipo is null)
            erros.Append("Tipo inválido.");

        if (valor is null or 0)
            erros.Append("Tipo inválido.");

        if (quantidade is null or <= 0)
            erros.Append("Quantidade inválida.");

        if (erros.Length > 0)
            throw new MaterialInvalidoException(erros.ToString());

        if (quantidade is > 0)
        {
            Estoque = new List<Estoque>();
            Estoque.Add(new Estoque(this, EntradaSaidaEnum.Entrada, quantidade.Value));
        }

        Quantidade = quantidade.Value;
        Nome = nome;
        Descricao = descricao;
        Tipo = tipo.Value;
        Valor = valor.Value;
    }

    public void SetNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            return;

        Nome = nome;
    }

    public void SetDescricao(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            return;

        Descricao = descricao;
    }

    public void SetTipo(TipoMaterialEnum? tipo)
    {
        if (tipo is null)
            return;

        Tipo = tipo.Value;
    }

    public void SetValor(double? valor)
    {
        if (valor is null or 0)
            return;

        Valor = valor.Value;
    }

    public void MovimentarEstoque(EntradaSaidaEnum? operacao, int? quantidade)
    {
        Estoque.Append(new Estoque(this, operacao, quantidade));

        Quantidade = (operacao is EntradaSaidaEnum.Entrada) ? Quantidade + quantidade.Value : Quantidade - quantidade.Value;
    }
}