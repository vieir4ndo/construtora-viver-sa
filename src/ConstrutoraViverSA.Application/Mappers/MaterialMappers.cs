using AutoMapper;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Application.Mappers;

public class MaterialMappers : Profile
{
    public MaterialMappers()
    {
        CreateMap<Material, MaterialDto>().ReverseMap();
    }
}