using System.ComponentModel.DataAnnotations;

namespace ConstrutoraViverSA.Domain.Enums;

public enum EntradaSaidaEnum
{
    [Display(Name = "Entrada")] Entrada = 2,

    [Display(Name = "Saída")] Saida = 1,
}