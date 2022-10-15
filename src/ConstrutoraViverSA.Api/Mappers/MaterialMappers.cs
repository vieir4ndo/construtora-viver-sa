using AutoMapper;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Api.Mappers;

public class MaterialMappers : Profile
{
    public MaterialMappers()
    {
        CreateMap<MaterialRequest, MaterialDto>().ReverseMap();
        CreateMap<MaterialRequest, EditarMaterialDto>().ReverseMap();
    }
}