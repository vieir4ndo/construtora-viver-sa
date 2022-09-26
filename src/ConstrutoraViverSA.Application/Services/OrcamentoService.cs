using ConstrutoraViverSA.Domain;
using System.Collections.Generic;
using ConstrutoraViverSA.Application.Interfaces;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Exceptions;
using ConstrutoraViverSA.Repository.Interfaces;

namespace ConstrutoraViverSA.Application.Services;

public class OrcamentoService : IOrcamentoService
{
    private readonly IOrcamentoRepository _repository;

    public OrcamentoService(IOrcamentoRepository repository)
    {
        _repository = repository;
    }

    public List<Orcamento> BuscarTodos()
    {
        return _repository.BuscarTodos();
    }

    public Orcamento BuscarPorId(long buscaId)
    {
        var orcamento = _repository.BuscarPorId(buscaId);

        if (orcamento is null)
        {
            throw new NaoEncontradoException("Orçamento não encontrado");
        }

        return orcamento;
    }

    public void Adicionar(OrcamentoDto dto)
    {
        _repository.Adicionar(dto.DtoParaDominio());
    }

    public void Excluir(long idExcluir)
    {
        var orcamento = BuscarPorId(idExcluir);

        _repository.Excluir(orcamento);
    }

    public void Editar(long id, OrcamentoDto orcamentolAtualizado)
    {
        var orcamento = BuscarPorId(id);

        orcamento.Descricao = orcamentolAtualizado.Descricao ?? orcamento.Descricao;
        orcamento.Endereco = orcamentolAtualizado.Endereco ?? orcamento.Endereco;
        orcamento.TipoObra = orcamentolAtualizado.TipoObra ?? orcamento.TipoObra;
        orcamento.DataEmissao = orcamentolAtualizado.DataEmissao ?? orcamento.DataEmissao;
        orcamento.DataValidade = orcamentolAtualizado.DataValidade ?? orcamento.DataValidade;
        orcamento.Valor = orcamentolAtualizado.Valor ?? orcamento.Valor;

        _repository.Editar(orcamento);
    }
}