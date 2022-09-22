using System.ComponentModel.DataAnnotations;

namespace ConstrutoraViverSA.Domain.Enums
{
    public enum GeneroEnum
    {
        [Display(Name="Masculino")]
        Masculino = 0,

        [Display(Name ="Feminino")]
        Feminino = 1,

        [Display(Name ="Não-binário")]
        NaoBinario = 3,

        [Display(Name ="Outro")]
        Outro = 4
    }
}
