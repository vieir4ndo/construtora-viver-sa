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

        if (obra.Funcionarios is { Count: > 0 })
        {
            foreach (var funcionario in obra.Funcionarios!)
            {
                dto.Funcionarios!.Add(funcionario.Id);
            } 
        }

        if (obra.ObraMateriais is not { Count: > 0 }) return dto;
        foreach (var materialId in obra.ObraMateriais!.Select(x => x.MaterialId).Distinct())
        {
            var entrada = obra.ObraMateriais
                .Where(x => x.MaterialId == materialId && x.Operacao == EntradaSaida.Entrada)
                .Sum(x => x.Quantidade);

            var saida = obra.ObraMateriais
                .Where(x => x.MaterialId == materialId && x.Operacao == EntradaSaida.Saida)
                .Sum(x => x.Quantidade);
                    
            var saldoMateriasObra = entrada - saida;

            dto.Materiais!.Add(materialId, saldoMateriasObra);
        }

        return dto;
    }
}