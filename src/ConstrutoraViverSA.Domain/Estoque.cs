using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;

namespace ConstrutoraViverSA.Domain;

public sealed class Estoque
{
    public long Id;
    public long MaterialId;
    public Material Material { get; }
    public EntradaSaida Operacao { get; }
    public int Quantidade { get; }
    public DateTime DataHora { get; }

    [ExcludeFromCodeCoverage]
    public Estoque()
    {
    }

    public Estoque(Material material, EntradaSaida? operacao, int? quantidade)
    {
        var erros = new StringBuilder();
        
        if (material is null)
            erros.Append("Material inválido.");

        if (operacao is null)
            erros.Append("Operação inválida.");

        if (quantidade is null or 0 or < 0)
            erros.Append("Quantidade inválida.");

        if (erros.Length > 0)
            throw new EstoqueInvalidoException(erros.ToString());

        Material = material;
        Operacao = operacao!.Value;
        Quantidade = quantidade!.Value;
        DataHora = DateTime.Now;
    }
}