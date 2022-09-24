using System.Collections.Generic;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Repository.Interfaces
{
    public interface IMaterialRepository
    {
        List<Material> BuscarMateriais();
        Material BuscarMaterialPorId(long buscaId);
        void AdicionarMaterial(MaterialDto material);
        void ExcluirMaterial(Material material);
        void AlterarMaterial(Material material);
    }
}