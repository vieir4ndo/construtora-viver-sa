using System.Collections.Generic;
using System.Linq;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Infrastructure;
using ConstrutoraViverSA.Repository.Interfaces;

namespace ConstrutoraViverSA.Repository.Repositories;

public class ObraMaterialRepository : IObraMaterialRepository
{
    private readonly ApplicationContext _database;

    public ObraMaterialRepository(ApplicationContext applicationContext)
    {
        _database = applicationContext;
    }

    public List<ObraMaterial> BuscarTodos()
    {
        return _database.ObraMaterial
            .ToList();
    }

    public ObraMaterial BuscarPorId(long buscaId)
    {
        return _database.ObraMaterial
            .FirstOrDefault(p => p.Id == buscaId);
    }

    public void Adicionar(ObraMaterial obj)
    {
        _database.ObraMaterial.Add(obj);
        _database.SaveChanges();
    }

    public void Excluir(ObraMaterial obj)
    {
        _database.ObraMaterial.Remove(obj);
        _database.SaveChanges();
    }

    public void Editar(ObraMaterial obj)
    {
        _database.ObraMaterial.Update(obj);
        _database.SaveChanges();
    }

    public ObraMaterial BuscarPorObraIdEMaterialId(long obraId, long materialId)
    {
        return _database.ObraMaterial
            .FirstOrDefault(x => x.ObraId == obraId && x.MaterialId == materialId);
    }
}