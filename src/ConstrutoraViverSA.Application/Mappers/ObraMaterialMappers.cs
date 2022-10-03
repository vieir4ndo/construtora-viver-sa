using AutoMapper;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Application.Mappers;

public class ObraMaterialMappers : Profile
{
    public ObraMaterialMappers()
    {
        CreateMap<ObraMaterial, ObraMaterialDto>().ReverseMap();
    }
}