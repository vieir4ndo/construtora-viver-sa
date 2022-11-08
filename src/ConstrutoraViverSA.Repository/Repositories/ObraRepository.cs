using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Infrastructure;
using ConstrutoraViverSA.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConstrutoraViverSA.Repository.Repositories;

[ExcludeFromCodeCoverage]
public class ObraRepository : IObraRepository
{
    private readonly ApplicationContext _database;

    public ObraRepository(ApplicationContext applicationContext)
    {
        _database = applicationContext;
    }

    public List<Obra> BuscarTodos()
    {
        return _database.Obra
            .Where(p => p.Id > 0)
            .Include(x => x.Funcionarios)
            .Include(x => x.ObraMateriais)
            .OrderBy(p => p.Id)
            .ToList();
    }

    public Obra BuscarPorId(long buscaId)
    {
        return _database.Obra
            .Where(p => p.Id == buscaId)
            .Include(p => p.Orcamento)
            .Include(p => p.Funcionarios)
            .Include(p => p.ObraMateriais)
            .ThenInclude(p => p.Material)
            .FirstOrDefault();
    }

    public void Adicionar(Obra obra)
    {
        _database.Obra.Add(obra);
        _database.SaveChanges();
    }

    public void Excluir(Obra obra)
    {
        _database.Obra.Remove(obra);
        _database.SaveChanges();
    }

    public void Editar(Obra obra)
    {
        _database.Obra.Update(obra);
        _database.SaveChanges();
    }
}