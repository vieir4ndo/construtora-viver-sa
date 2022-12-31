using System.ComponentModel.DataAnnotations;

namespace ConstrutoraViverSA.Domain.Enums;

public enum Genero
{
    [Display(Name = "Masculino")] Masculino = 1,

    [Display(Name = "Feminino")] Feminino = 2,

    [Display(Name = "Não-binário")] NaoBinario = 3,

    [Display(Name = "Outro")] Outro = 4
}