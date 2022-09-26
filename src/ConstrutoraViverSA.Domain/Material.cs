using ConstrutoraViverSA.Domain.Enums;
using System.Collections.Generic;

namespace ConstrutoraViverSA.Domain;

public class Material
{
    public long Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public TipoMaterialEnum? Tipo { get; set; }
    public double? Valor { get; set; }
    public int Quantidade { get; set; }
    public virtual ICollection<Estoque> Estoque { get; set; }
    public virtual ICollection<ObraMaterial> ObraMateriais { get; set; }

    public Material()
    {
    }

    public Material
    (
        string nome,
        string descricao,
        TipoMaterialEnum tipo,
        double valor
    )
    {
        Nome = nome;
        Descricao = descricao;
        Tipo = tipo;
        Valor = valor;
    }
}