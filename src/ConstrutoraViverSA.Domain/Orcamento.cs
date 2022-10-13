using System;
using System.Text;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;

namespace ConstrutoraViverSA.Domain;

public class Orcamento
{
    public long Id { get; private set; }
    public string Descricao { get; private set; }
    public string Endereco { get; private set; }
    public TipoObraEnum? TipoObra { get; private set; }
    public DateTime? DataEmissao { get; private set; }
    public DateTime? DataValidade { get; private set; }
    public double? Valor { get; private set; }
    public virtual Obra Obra { get; private set; }

    public Orcamento()
    {
    }

    public Orcamento(string descricao, string endereco, TipoObraEnum? tipoObra, DateTime? dataEmissao,
        DateTime? dataValidade, double? valor, Obra? obra)
    {
        var erros = new StringBuilder();
        
        if (string.IsNullOrWhiteSpace(descricao))
            erros.Append("Descrção inválida.");

        if (string.IsNullOrWhiteSpace(endereco))
            erros.Append("Endereço inválido.");

        if (tipoObra is null)
            erros.Append("Tipo Obra inválido.");

        if (dataEmissao is null)
            erros.Append("Data Emissão inválida.");
        
        if (dataValidade is null)
            erros.Append("Data Validade inválida.");

        if (dataValidade.Value < dataEmissao.Value)
            erros.Append("Data de Emissão e Data de Validade em intervalo inválido.");

        if (valor is null or <= 0)
            erros.Append("Valor inválido.");

        if (erros.Length > 0)
            throw new OrcamentoInvalidoException(erros.ToString());
        
        Descricao = descricao;
        Endereco = endereco;
        TipoObra = tipoObra.Value;
        DataEmissao = dataEmissao.Value;
        DataValidade = dataValidade.Value;
        Valor = valor.Value;
        Obra = obra;
    }

    public void SetDescricao(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            return;

        Descricao = descricao;
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

        TipoObra = tipoObra;
    }

    public void SetDataEmissao(DateTime? dataEmissao)
    {
        if (dataEmissao is null || dataEmissao > DataValidade)
            return;

        DataEmissao = dataEmissao.Value;
    }

    public void SetDataValidade(DateTime? dataValidade)
    {
        if (dataValidade is null || dataValidade < DataEmissao)
            return;

        DataValidade = dataValidade.Value;
    }

    public void SetValor(double? valor)
    {
        if (valor is null or <= 0)
            return;

        Valor = valor;
    }
        

        

}