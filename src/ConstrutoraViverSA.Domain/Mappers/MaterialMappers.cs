using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Domain.Mappers;

public class MaterialMappers: AutoMapper.Profile
{
    public MaterialMappers()
    {
        CreateMap<Material, MaterialDto>().ReverseMap();
    }
}