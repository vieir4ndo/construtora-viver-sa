using AutoMapper;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Application.Mappers;

public class ObraMappers : Profile
{
    public ObraMappers()
    {
        CreateMap<Obra, ObraDto>().ReverseMap();
    }
}