using System;
using ConstrutoraViverSA.Domain.Enums;

namespace ConstrutoraViverSA.Domain.Dtos;

public class OrcamentoDto
{
    public string Descricao { get; set; }
    public string Endereco { get; set; }
    public TipoObraEnum? TipoObra { get; set; }
    public DateTime? DataEmissao { get; set; }
    public DateTime? DataValidade { get; set; }
    public double? Valor { get; set; }

    public Orcamento DtoParaDominio()
    {
        return new Orcamento(
            Descricao,
            Endereco,
            TipoObra,
            Convert.ToDateTime(DataEmissao),
            Convert.ToDateTime(DataValidade),
            Convert.ToDouble(Valor)
        );
    }
}