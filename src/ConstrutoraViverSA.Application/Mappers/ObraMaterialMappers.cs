using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Application.Mappers;

[ExcludeFromCodeCoverage]
public class ObraMaterialMappers : Profile
{
    public ObraMaterialMappers()
    {
        CreateMap<ObraMaterial, ObraMaterialDto>().ReverseMap();
    }
}