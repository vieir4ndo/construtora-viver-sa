using ConstrutoraViverSA.Domain;
using System.Collections.Generic;
using AutoMapper;
using ConstrutoraViverSA.Application.Interfaces;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Exceptions;
using ConstrutoraViverSA.Repository.Interfaces;

namespace ConstrutoraViverSA.Application.Services;

public class OrcamentoService : IOrcamentoService
{
    private readonly IOrcamentoRepository _repository;
    private readonly IMapper _mapper;

    public OrcamentoService(IOrcamentoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public List<OrcamentoDto> BuscarTodos()
    {
        var orcamentos = _repository.BuscarTodos();

        var listaOrcamentosDto = new List<OrcamentoDto>();
            
        orcamentos.ForEach(x => listaOrcamentosDto.Add(_mapper.Map<OrcamentoDto>(x)));

        return listaOrcamentosDto;
    }

    public OrcamentoDto BuscarPorId(long buscaId)
    {
        var orcamento = BuscarEntidadePorId(buscaId);

        return _mapper.Map<OrcamentoDto>(orcamento);
    }


    public Orcamento BuscarEntidadePorId(long buscaId)
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
        var orcamento = new Orcamento(dto.Descricao, dto.Endereco, dto.TipoObra, dto.DataEmissao, dto.DataValidade,
            dto.Valor);
        _repository.Adicionar(orcamento);
    }

    public void Excluir(long idExcluir)
    {
        var orcamento = BuscarEntidadePorId(idExcluir);

        _repository.Excluir(orcamento);
    }

    public void Editar(long id, OrcamentoDto orcamentolAtualizado)
    {
        var orcamento = BuscarEntidadePorId(id);

        orcamento.SetDescricao(orcamentolAtualizado.Descricao);
        orcamento.SetEndereco(orcamentolAtualizado.Endereco);
        orcamento.SetTipoObra(orcamentolAtualizado.TipoObra);
        orcamento.SetDataEmissao(orcamentolAtualizado.DataEmissao);
        orcamento.SetDataValidade(orcamentolAtualizado.DataValidade);
        orcamento.SetValor(orcamentolAtualizado.Valor);

        _repository.Editar(orcamento);
    }
}