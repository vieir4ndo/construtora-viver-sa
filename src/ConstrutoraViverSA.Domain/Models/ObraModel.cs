#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ConstrutoraViverSA.Domain.Enums;

namespace ConstrutoraViverSA.Domain.Models;

[ExcludeFromCodeCoverage]

public class ObraModel
{
    public string? Nome { get; set; }
    public string? Endereco { get; set; }
    public TipoObra? TipoObra { get; set; }
    public string? Descricao { get; set; }
    public double? Valor { get; set; }
    public DateTime? PrazoConclusao { get; set; }
    public long? OrcamentoId { get; set; }
    public List<Funcionario>? Funcionarios { get; set; }
    public List<Material>? Materiais { get; set; } 
}