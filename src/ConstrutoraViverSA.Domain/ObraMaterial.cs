#nullable enable
using System.Text;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;

namespace ConstrutoraViverSA.Domain;

public class ObraMaterial
{
    public long Id { get; private set; }
    public long ObraId { get; private set; }
    public virtual Obra Obra { get; private set; }
    public long MaterialId { get; private set; }
    public virtual Material Material { get; private set; }
    public int Quantidade { get; private set; }

    public ObraMaterial()
    {
        
    }

    public ObraMaterial(Obra? obra, Material? material, int? quantidade)
    {
        var erros = new StringBuilder();
        
        if (obra is null)
            erros.Append("Obra inválida.");

        if (material is null)
            erros.Append("Material inválido.");

        if (quantidade is null or <= 0)
            erros.Append("Quantidade inválida.");

        if (erros.Length > 0)
            throw new ObraMaterialInvalidaException(erros.ToString());
        
        Obra = obra;
        Material = material;
        Quantidade = quantidade.Value;
        Material.MovimentarEstoque(EntradaSaidaEnum.Saida, quantidade);
    }
    
    public void SetQuantidade(int? quantidade)
    {
        if (quantidade is null or <= 0)
            throw new ObraMaterialInvalidaException("Quantidade inválida.");

        Quantidade = quantidade.Value;
    }
    
}