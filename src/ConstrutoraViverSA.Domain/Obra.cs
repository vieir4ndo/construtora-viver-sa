﻿#nullable enable
using ConstrutoraViverSA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using ConstrutoraViverSA.Domain.Exceptions;

namespace ConstrutoraViverSA.Domain;

public class Obra
{
    public long Id { get; set; }
    public string Nome { get; private set; } = null!;
    public string Endereco { get; private set; } = null!;
    public TipoObra? TipoObra { get; private set; }
    public string Descricao { get; private set; } = null!;
    public double? Valor { get; private set; }
    public DateTime? PrazoConclusao { get; private set; }
    public Orcamento Orcamento { get; private set; } = null!;

    public long OrcamentoId { get; private set; }
    public ICollection<Funcionario>? Funcionarios { get; private set;  } = new Collection<Funcionario>();
    public ICollection<ObraMaterial>? ObraMateriais { get; private set; } = new Collection<ObraMaterial>();

    [ExcludeFromCodeCoverage]
    protected Obra()
    {
    }

    public Obra(string? nome, string? endereco, TipoObra? tipoObra, string? descricao,
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
        
        if (orcamento is null)
            erros.Append("Orcamento inválido.");

        if (orcamento is not null && (prazoConclusao is null || prazoConclusao.Value < orcamento!.DataValidade ))
        {
            erros.Append("Prazo conclusão inválido.");
        }

        if (erros.Length > 0)
            throw new ObraInvalidaException(erros.ToString());

        Nome = nome!;
        Endereco = endereco!;
        TipoObra = tipoObra;
        Descricao = descricao!;
        Valor = valor;
        PrazoConclusao = prazoConclusao;
        Orcamento = orcamento!;

        if (funcionarios is not null && funcionarios.Count is > 0)
        {
            foreach (var funcionario in funcionarios)
            {
                Funcionarios.Add(funcionario);
            }
        }

        if (materiais is not null && materiais.Count is > 0)
        {
            foreach (var material in materiais)
            {
                ObraMateriais!.Add(new ObraMaterial(this, material.Key, material.Value, EntradaSaida.Entrada));
            }
        }
    }

    public void SetNome(string? nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            return;

        Nome = nome;
    }

    public void SetEndereco(string? endereco)
    {
        if (string.IsNullOrWhiteSpace(endereco))
            return;

        Endereco = endereco;
    }

    public void SetTipoObra(TipoObra? tipoObra)
    {
        if (tipoObra is null)
            return;

        TipoObra = tipoObra.Value;
    }

    public void SetDescricao(string? descricao)
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
        if (prazoConclusao is null || prazoConclusao.Value < Orcamento.DataValidade)
            return;

        PrazoConclusao = prazoConclusao;
    }

    public void SetOrcamento(Orcamento? orcamento)
    {
        if (orcamento is null)
            return;

        Orcamento = orcamento;
    }

    public void AlocarFuncionario(Funcionario funcionario)
    {
        Funcionarios ??= new List<Funcionario>();
        
        if (Funcionarios.Contains(funcionario))
            throw new OperacaoInvalidaException(
                $"Funcionário {funcionario.Nome} já está alocado na obra {this.Nome}");
        
        Funcionarios.Add(funcionario);
    }

    public void DesalocarFuncionario(Funcionario funcionario)
    {
        if (Funcionarios is null || !Funcionarios.Contains(funcionario))
            throw new OperacaoInvalidaException($"Funcionário {funcionario.Nome} não está alocado na obra {this.Nome}");
        
        Funcionarios.Remove(funcionario);
    }

    public void AlocarMaterial(Material material, int? quantidade)
    {
        ObraMateriais ??= new List<ObraMaterial>();
        
        if (material.Quantidade < quantidade)
            throw new OperacaoInvalidaException(
                $"Não há itens suficientes em estoque do material {material.Nome} para alocar na obra {this.Nome}");
        
        ObraMateriais.Add(new ObraMaterial(this, material, quantidade, EntradaSaida.Entrada));
    }

    public void DesalocarMaterial(Material material, int? quantidade)
    {
        if (ObraMateriais is null)
            throw new OperacaoInvalidaException(
                $"Material {material.Nome} não está alocado na obra {this.Nome}");
        
        if (quantidade is null or <= 0)
            throw new OperacaoInvalidaException("Quantidade inválida");
        
        var entrada = ObraMateriais.Where(x => x.Operacao == EntradaSaida.Entrada && x.MaterialId == material.Id).Sum(x => x.Quantidade);
        var saida = ObraMateriais.Where(x => x.Operacao == EntradaSaida.Saida && x.MaterialId == material.Id).Sum(x => x.Quantidade);

        var saldoDeMateriaisNaObra = entrada - saida;
        
        if (saldoDeMateriaisNaObra <= 0)
            throw new OperacaoInvalidaException(
                $"Material {material.Nome} não está alocado na obra {this.Nome}");

        if (quantidade > saldoDeMateriaisNaObra)
            throw new OperacaoInvalidaException(
                $"Material {material.Nome} está alocado na obra {this.Nome} com apenas {saldoDeMateriaisNaObra} itens");

        ObraMateriais.Add(new ObraMaterial(this, material, quantidade, EntradaSaida.Saida));
    }
}