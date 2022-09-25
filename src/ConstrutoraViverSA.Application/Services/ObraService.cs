using ConstrutoraViverSA.Domain;
using System.Collections.Generic;
using ConstrutoraViverSA.Application.Interfaces;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Exceptions;
using ConstrutoraViverSA.Repository.Interfaces;

namespace ConstrutoraViverSA.Application.Services
{
    public class ObraService : IObraService
    {
        private readonly IObraRepository _repository;
        public ObraService(IObraRepository repository)
        {
            _repository = repository;
        }

        public List<Obra> BuscarTodos()
        {
            return _repository.BuscarTodos();
        }

        public Obra BuscarPorId(long buscaId)
        {
            var obra = _repository.BuscarPorId(buscaId);
            
            if (obra is null)
            {
                throw new NaoEncontradoException("Obra não encontrada");
            }

            return obra;
        }

        public void Adicionar(ObraDto obra)
        {
            _repository.Adicionar(obra.DtoParaDominio());
        }
        public void Excluir(long idExcluir)
        {
            var obra = BuscarPorId(idExcluir);

            _repository.Excluir(obra);
        }

        public void Editar(long id, ObraDto obralAtualizado)
        {
            var obra = BuscarPorId(id);

            obra.Nome = obralAtualizado.Nome ?? obra.Nome;
            obra.Descricao = obralAtualizado.Descricao ?? obra.Descricao;
            obra.Endereco = obralAtualizado.Endereco ?? obra.Endereco;
            obra.TipoObra = obralAtualizado.TipoObra ?? obra.TipoObra;
            obra.Valor = obralAtualizado.Valor ?? obra.Valor;
            obra.PrazoConclusao = obralAtualizado.PrazoConclusao ?? obra.PrazoConclusao;

            _repository.Editar(obra);
        }
    }
}