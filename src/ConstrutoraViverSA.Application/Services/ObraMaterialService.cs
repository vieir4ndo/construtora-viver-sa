using System.Collections.Generic;
using ConstrutoraViverSA.Application.Interfaces;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Exceptions;
using ConstrutoraViverSA.Repository.Interfaces;

namespace ConstrutoraViverSA.Application.Services
{
    public class ObraMaterialService : IObraMaterialService
    {
        private readonly IObraMaterialRepository _repository;

        public ObraMaterialService(
            IObraMaterialRepository repository
        )
        {
            _repository = repository;
        }

        public List<ObraMaterial> BuscarTodos()
        {
            return _repository.BuscarTodos();
        }

        public ObraMaterial BuscarPorId(long buscaId)
        {
            var obraMaterial = _repository.BuscarPorId(buscaId);

            if (obraMaterial is null) throw new NaoEncontradoException("ObraMaterial não encontrado");

            return obraMaterial;
        }

        public void Adicionar(ObraMaterialDto dto)
        {
            _repository.Adicionar(dto.DtoParaDominio());
        }

        public void Excluir(long idExcluir)
        {
            var obraMaterial = BuscarPorId(idExcluir);

            _repository.Excluir(obraMaterial);
        }

        public void Editar(long id, ObraMaterialDto dto)
        {
            var obraMaterial = BuscarPorId(id);

            if (dto.MaterialId != null && dto.MaterialId != obraMaterial.MaterialId)
                obraMaterial.MaterialId = dto.MaterialId;

            if (dto.ObraId != null && dto.ObraId != obraMaterial.ObraId) obraMaterial.ObraId = dto.ObraId;

            _repository.Editar(obraMaterial);
        }

        public ObraMaterial BuscarPorObraIdEMaterialId(long obraId, long materialId)
        {
            return _repository.BuscarPorObraIdEMaterialId(obraId, materialId);
        }
    }
}