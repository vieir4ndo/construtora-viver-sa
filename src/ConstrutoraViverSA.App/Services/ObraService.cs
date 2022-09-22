using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Infraestrutura;
using System.Collections.Generic;
using System.Linq;

namespace ConstrutoraViverSA.App.Services
{
    public class ObraService
    {
        private readonly ApplicationContext _database;
        public ObraService()
        {
            _database = new ApplicationContext();
        }

        public List<Obra> BuscarObras()
        {
            return _database.Obras
                .Where(p => p.Id > 0)
                .OrderBy(p => p.Id)
                .ToList();
        }

        public Obra BuscarObraPorId(long BuscaId)
        {
            return _database.Obras
                .FirstOrDefault(p => p.Id == BuscaId);
        }

        public void AdicionarObra(Obra Obra)
        {
            _database.Obras.Add(Obra);
            _database.SaveChanges();
        }
        public void ExcluirObra(long IdExcluir)
        {
            Obra Obra = _database.Obras.Find(IdExcluir);

            _database.Obras.Remove(Obra);
            _database.SaveChanges();
        }

        public void AlterarObra(long Id, Obra ObralAtualizado)
        {
            Obra Obra = _database.Obras.Find(Id);

            Obra.Nome = ObralAtualizado.Nome;
            Obra.Descricao = ObralAtualizado.Descricao;
            Obra.Endereco = ObralAtualizado.Endereco;
            Obra.TipoObra = ObralAtualizado.TipoObra;
            Obra.Valor = ObralAtualizado.Valor;
            Obra.PrazoConclusao = ObralAtualizado.PrazoConclusao;

            _database.Obras.Update(Obra);
            _database.SaveChanges();
        }
    }
}