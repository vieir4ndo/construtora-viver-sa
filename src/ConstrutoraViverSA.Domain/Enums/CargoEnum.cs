using System.ComponentModel.DataAnnotations;

namespace ConstrutoraViverSA.Domain.Enums;

public enum CargoEnum
{
    [Display(Name = "Sócio-Proprietário")] SocioProprietário = 30,

    [Display(Name = "Recepcionista")] Recepcionista = 1,

    [Display(Name = "Auxiliar de Suprimentos")]
    AuxiliarSuprimentos = 2,

    [Display(Name = "Assistente de Suprimentos")]
    AssistenteSuprimentos = 3,

    [Display(Name = "Aualista de Suprimentos")]
    AualistaSuprimentos = 4,

    [Display(Name = "Gerente de Suprimentos")]
    GerenteSuprimentos = 5,

    [Display(Name = "Auxiliar de Recursos Humanos")]
    AuxiliarRecursosHumanos = 6,

    [Display(Name = "Assistente de Recursos Humanos")]
    AssistenteRecursosHumanos = 7,

    [Display(Name = "Analista de Recursos Humanos")]
    AnalistaRecursosHumanos = 8,

    [Display(Name = "Gerente de Recursos Humanos")]
    GerenteRecursosHumanos = 9,

    [Display(Name = "Técnico em Saúde e Segurança do Trabalho")]
    TecnicoSaúdeSegurançaTrabalho = 10,

    [Display(Name = "Auxiliar Financeiro")]
    AuxiliarFinanceiro = 11,

    [Display(Name = "Assistente Financeiro")]
    AssistenteFinanceiro = 12,

    [Display(Name = "Analista Financeiro")]
    AnalistaFinanceiro = 13,

    [Display(Name = "Gerente Financeiro")] GerenteFinanceiro = 14,

    [Display(Name = "Almoxarife")] Almoxarife = 15,

    [Display(Name = "Analista de Qualidade")]
    AnalistaQualidade = 16,

    [Display(Name = "Gerente de Qualidade")]
    GerenteQualidade = 17,

    [Display(Name = "Menor Aprendiz")] MenorAprendiz = 18,

    [Display(Name = "Estágiario")] Estágiario = 19,

    [Display(Name = "Engenheiro Cívil")] EngenheiroCivil = 20,

    [Display(Name = "Arquiteto")] Arquiteto = 21,

    [Display(Name = "Técnico em Edificações")]
    TecnicoEdificações = 22,

    [Display(Name = "Diretor de Fábrica")] DiretorFabrica = 23,

    [Display(Name = "Pedreiro")] Pedreiro = 24,

    [Display(Name = "Pedreiro de Acabamento")]
    PedreiroAcabamento = 25,

    [Display(Name = "Carpinteiro")] Carpinteiro = 26,

    [Display(Name = "Mestre de Obras")] MestreObras = 27,

    [Display(Name = "Servente")] Servente = 28,

    [Display(Name = "Armador")] Armador = 29,
}