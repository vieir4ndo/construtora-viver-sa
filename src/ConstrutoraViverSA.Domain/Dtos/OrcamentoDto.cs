using System;
using System.Diagnostics.CodeAnalysis;
using ConstrutoraViverSA.Domain.Enums;

namespace ConstrutoraViverSA.Domain.Dtos;

[ExcludeFromCodeCoverage]
public class OrcamentoDto
{
    public string Descricao { get; set; }
    public string Endereco { get; set; }
    public TipoObra? TipoObra { get; set; }
    public DateTime? DataEmissao { get; set; }
    public DateTime? DataValidade { get; set; }
    public double? Valor { get; set; }
}