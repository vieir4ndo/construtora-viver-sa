using System.Linq;
using AutoMapper;
using ConstrutoraViverSA.Application.Interfaces;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Enums;

namespace ConstrutoraViverSA.Application.Mappers;

public class ObraParaObraDto : IObraParaObraDto
{
    private readonly IMapper _mapper;

    public ObraParaObraDto(IMapper mapper)
    {
        _mapper = mapper;
    }

    public ObraDto Mapear(Obra obra)
    {
        var dto = _mapper.Map<ObraDto>(obra);

        if (obra.Funcionarios != null && obra.Funcionarios.Count > 0)
        {
            foreach (var funcionario in obra.Funcionarios!)
            {
                dto.Funcionarios!.Add(funcionario.Id);
            } 
        }

        if (obra.ObraMateriais != null && obra.ObraMateriais.Count > 0)
        {
            foreach (var materialId in obra.ObraMateriais!.Select(x => x.MaterialId).Distinct())
            {
                int entrada = obra.ObraMateriais
                    .Where(x => x.MaterialId == materialId && x.Operacao == EntradaSaidaEnum.Entrada)
                    .Sum(x => x.Quantidade);

                int saida = obra.ObraMateriais
                    .Where(x => x.MaterialId == materialId && x.Operacao == EntradaSaidaEnum.Saida)
                    .Sum(x => x.Quantidade);
                    
                var saldoMateriasObra = entrada - saida;

                dto.Materiais!.Add(materialId, saldoMateriasObra);
            }
        }

        return dto;
    }
}