using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Infraestrutura;
using System.Collections.Generic;
using System.Linq;
using ConstrutoraViverSA.Application.Interfaces;

namespace ConstrutoraViverSA.Application.Services
{
    public class ObraService : IObraService
    {
        private readonly ApplicationContext _database;
        public ObraService(ApplicationContext applicationContext)
        {
            _database = applicationContext;
        }

        public List<Obra> BuscarObras()
        {
            return _database.Obras
                .Where(p => p.Id > 0)
                .OrderBy(p => p.Id)
                .ToList();
        }

        public Obra BuscarObraPorId(long buscaId)
        {
            return _database.Obras
                .FirstOrDefault(p => p.Id == buscaId);
        }

        public void AdicionarObra(Obra Obra)
        {
            _database.Obras.Add(Obra);
            _database.SaveChanges();
        }
        public void ExcluirObra(long idExcluir)
        {
            Obra Obra = _database.Obras.Find(idExcluir);

            _database.Obras.Remove(Obra);
            _database.SaveChanges();
        }

        public void AlterarObra(long id, Obra obralAtualizado)
        {
            Obra Obra = _database.Obras.Find(id);

            Obra.Nome = obralAtualizado.Nome;
            Obra.Descricao = obralAtualizado.Descricao;
            Obra.Endereco = obralAtualizado.Endereco;
            Obra.TipoObra = obralAtualizado.TipoObra;
            Obra.Valor = obralAtualizado.Valor;
            Obra.PrazoConclusao = obralAtualizado.PrazoConclusao;

            _database.Obras.Update(Obra);
            _database.SaveChanges();
        }
    }
}