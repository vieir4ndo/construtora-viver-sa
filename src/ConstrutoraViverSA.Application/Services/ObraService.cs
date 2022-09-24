using System;
using ConstrutoraViverSA.Domain;
using System.Collections.Generic;
using ConstrutoraViverSA.Application.Interfaces;
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

        public List<Obra> BuscarObras()
        {
            return _repository.BuscarObras();
        }

        public Obra BuscarObraPorId(long buscaId)
        {
            var obra = _repository.BuscarObraPorId(buscaId);
            
            if (obra is null)
            {
                throw new Exception("Obra não encontrada");
            }

            return obra;
        }

        public void AdicionarObra(Obra obra)
        {
            _repository.AdicionarObra(obra);
        }
        public void ExcluirObra(long idExcluir)
        {
            var obra = BuscarObraPorId(idExcluir);

            _repository.ExcluirObra(obra);
        }

        public void AlterarObra(long id, Obra obralAtualizado)
        {
            var obra = BuscarObraPorId(id);

            obra.Nome = obralAtualizado.Nome ?? obra.Nome;
            obra.Descricao = obralAtualizado.Descricao ?? obra.Descricao;
            obra.Endereco = obralAtualizado.Endereco ?? obra.Endereco;
            obra.TipoObra = obralAtualizado.TipoObra ?? obra.TipoObra;
            obra.Valor = obralAtualizado.Valor ?? obra.Valor;
            obra.PrazoConclusao = obralAtualizado.PrazoConclusao ?? obra.PrazoConclusao;

            _repository.AlterarObra(obra);
        }
    }
}