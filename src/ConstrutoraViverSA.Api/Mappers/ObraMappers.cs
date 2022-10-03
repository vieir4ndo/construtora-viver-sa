using AutoMapper;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Api.Mappers;

public class ObraMappers : Profile
{
    public ObraMappers()
    {
        CreateMap<ObraRequest, ObraDto>().ReverseMap();
    }
}