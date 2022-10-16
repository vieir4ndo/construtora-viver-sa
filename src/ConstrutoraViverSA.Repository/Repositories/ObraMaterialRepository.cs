using System.Collections.Generic;
using System.Linq;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Enums;
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

    public int BuscarQuantidadeDeMateriaisPorObraIdEMaterialId(long obraId, long materialId)
    {
        var entrada = _database.ObraMaterial.Where(x => x.ObraId == obraId && x.MaterialId == materialId && x.Operacao == EntradaSaidaEnum.Entrada).Sum(x => x.Quantidade);
        var saida = _database.ObraMaterial.Where(x => x.ObraId == obraId && x.MaterialId == materialId && x.Operacao == EntradaSaidaEnum.Saida).Sum(x => x.Quantidade);

        return entrada - saida;
    }
}