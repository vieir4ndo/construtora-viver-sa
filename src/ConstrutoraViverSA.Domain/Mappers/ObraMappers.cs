using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Domain.Mappers;

public class ObraMappers: AutoMapper.Profile
{
    public ObraMappers()
    {
        CreateMap<Obra, ObraDto>().ReverseMap();
    }
}