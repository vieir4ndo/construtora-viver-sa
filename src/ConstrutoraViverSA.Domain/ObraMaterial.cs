#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;

namespace ConstrutoraViverSA.Domain;

public sealed class ObraMaterial
{
    public long Id { get; set; }
    public long ObraId { get; private set; }
    public Obra Obra { get; private set; }
    public long MaterialId { get; private set; }
    public Material Material { get; }
    public int Quantidade { get; }
    public DateTime DataHora { get; }
    public EntradaSaida Operacao { get; }

    [ExcludeFromCodeCoverage]
    public ObraMaterial()
    {
        
    }

    public ObraMaterial(Obra? obra, Material? material, int? quantidade, EntradaSaida? operacao)
    {
        var erros = new StringBuilder();
        
        if (obra is null)
            erros.Append("Obra inválida.");

        if (material is null)
            erros.Append("Material inválido.");

        if (quantidade is null or <= 0)
            erros.Append("Quantidade inválida.");

        if (operacao is null)
            erros.Append("Operacao inválida");

        if (erros.Length > 0)
            throw new ObraMaterialInvalidaException(erros.ToString());
        
        Obra = obra!;
        ObraId = obra!.Id;
        Material = material!;
        MaterialId = material!.Id;
        Quantidade = quantidade!.Value;
        Operacao = operacao!.Value;
        DataHora = DateTime.Now;
        Material.MovimentarEstoque((operacao is EntradaSaida.Entrada) ? EntradaSaida.Saida : EntradaSaida.Entrada, quantidade);
    }

}