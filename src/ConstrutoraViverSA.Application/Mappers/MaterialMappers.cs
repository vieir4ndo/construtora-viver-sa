using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Application.Mappers;

[ExcludeFromCodeCoverage]
public class MaterialMappers : Profile
{
    public MaterialMappers()
    {
        CreateMap<Material, MaterialDto>().ReverseMap();
        CreateMap<Material, EditarMaterialDto>().ReverseMap();
    }
}