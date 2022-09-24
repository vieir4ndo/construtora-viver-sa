using System.Collections.Generic;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Application.Interfaces
{
    public interface IMaterialService
    {
        List<Material> BuscarMateriais();
        Material BuscarMaterialPorId(long buscaId);
        void AdicionarMaterial(MaterialDto material);
        void ExcluirMaterial(long idExcluir);
        void AlterarMaterial(long id, MaterialDto materialAtualizado);
    }
}