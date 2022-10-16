using System;
using System.Diagnostics.CodeAnalysis;
using ConstrutoraViverSA.Domain.Enums;

namespace ConstrutoraViverSA.Domain.Dtos;

[ExcludeFromCodeCoverage]
public class ObraDto
{
    public string Nome { get; set; }
    public string Endereco { get; set; }
    public TipoObraEnum? TipoObra { get; set; }
    public string Descricao { get; set; }
    public double? Valor { get; set; }
    public DateTime? PrazoConclusao { get; set; }
    public long? OrcamentoId { get; set; }
}