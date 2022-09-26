using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Domain.Mappers;

public class OrcamentoMappers: AutoMapper.Profile
{
    public OrcamentoMappers()
    {
        CreateMap<Orcamento, OrcamentoDto>().ReverseMap();
    }
}