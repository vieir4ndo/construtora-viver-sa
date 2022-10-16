using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Application.Mappers;

[ExcludeFromCodeCoverage]
public class ObraMappers : Profile
{
    public ObraMappers()
    {
        CreateMap<Obra, ObraDto>().ReverseMap();
    }
}