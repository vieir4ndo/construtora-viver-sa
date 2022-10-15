#nullable enable
using ConstrutoraViverSA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using ConstrutoraViverSA.Domain.Exceptions;

namespace ConstrutoraViverSA.Domain;

public class Obra
{
    public long Id { get; private set; }
    public string Nome { get; private set; }
    public string Endereco { get; private set; }
    public TipoObraEnum? TipoObra { get; private set; }
    public string Descricao { get; private set; }
    public double? Valor { get; private set; }
    public DateTime? PrazoConclusao { get; private set; }
    public virtual Orcamento Orcamento { get; private set; }
    public long OrcamentoId { get; private set; }
    public virtual ICollection<Funcionario> Funcionarios { get; private set; }
    public virtual ICollection<ObraMaterial> ObraMateriais { get; private set; }

    public Obra()
    {
    }

    public Obra(string nome, string endereco, TipoObraEnum? tipoObra, string descricao,
        double? valor, DateTime? prazoConclusao, Orcamento? orcamento, List<Funcionario>? funcionarios,
        Dictionary<Material, int>? materiais)
    {
        var erros = new StringBuilder();

        if (string.IsNullOrWhiteSpace(nome))
            erros.Append("Nome inválido.");

        if (string.IsNullOrWhiteSpace(endereco))
            erros.Append("Endereço inválido.");

        if (tipoObra is null)
            erros.Append("Tipo Obra inválido.");

        if (string.IsNullOrWhiteSpace(descricao))
            erros.Append("Descrição inválida.");

        if (valor is null or <= 0)
            erros.Append("Valor inválido.");

        if (prazoConclusao is null || prazoConclusao.Value <= DateTime.Today)
            erros.Append("Prazo conclusão inválido.");

        if (orcamento is null)
            erros.Append("Orçcamento inválido.");

        if (erros.Length > 0)
            throw new ObraInvalidaException(erros.ToString());

        Nome = nome;
        Endereco = endereco;
        TipoObra = tipoObra;
        Descricao = descricao;
        Valor = valor;
        PrazoConclusao = prazoConclusao;
        Orcamento = orcamento;

        if (funcionarios is not null && funcionarios.Count is > 0)
        {
            //Funcionarios = new List<Funcionario>();
            foreach (var funcionario in funcionarios)
            {
                Funcionarios.Add(funcionario);
            }
        }

        if (materiais is not null && materiais.Count is > 0)
        {
            //ObraMateriais = new List<ObraMaterial>();
            foreach (var material in materiais)
            {
                ObraMateriais.Add(new ObraMaterial(this, material.Key, material.Value));
            }
        }
    }

    public void SetNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            return;

        Nome = nome;
    }

    public void SetEndereco(string endereco)
    {
        if (string.IsNullOrWhiteSpace(endereco))
            return;

        Endereco = endereco;
    }

    public void SetTipoObra(TipoObraEnum? tipoObra)
    {
        if (tipoObra is null)
            return;

        TipoObra = tipoObra.Value;
    }

    public void SetDescricao(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            return;

        Descricao = descricao;
    }

    public void SetValor(double? valor)
    {
        if (valor is null or <= 0)
            return;

        Valor = valor.Value;
    }

    public void SetPrazoConclusao(DateTime? prazoConclusao)
    {
        if (prazoConclusao is null || prazoConclusao.Value <= DateTime.Today)
            return;

        PrazoConclusao = prazoConclusao;
    }

    public void SetOrcamento(Orcamento? orcamento)
    {
        if (orcamento is null)
            return;

        Orcamento = orcamento;
    }

    public void AlocarFuncionario()
    {
    }

    public void DesalocarFuncionario()
    {
    }

    public void AlocarMaterial()
    {
    }

    public void DesalocarMaterial()
    {
    }
}