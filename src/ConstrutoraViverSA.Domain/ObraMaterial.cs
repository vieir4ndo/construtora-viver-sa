#nullable enable
using System;
using System.Text;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;

namespace ConstrutoraViverSA.Domain;

public sealed class ObraMaterial
{
    public long Id;
    
    public long ObraId;
    public Obra Obra { get; }
    
    public long MaterialId;
    public Material Material { get; }
    public int Quantidade { get; }
    public DateTime DataHora { get; }
    public EntradaSaidaEnum Operacao { get; }

    public ObraMaterial()
    {
        
    }

    public ObraMaterial(Obra? obra, Material? material, int? quantidade, EntradaSaidaEnum? operacao)
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
        Material = material!;
        Quantidade = quantidade!.Value;
        Operacao = operacao!.Value;
        DataHora = DateTime.Now;
        Material.MovimentarEstoque((operacao is EntradaSaidaEnum.Entrada) ? EntradaSaidaEnum.Saida : EntradaSaidaEnum.Entrada, quantidade);
    }

}