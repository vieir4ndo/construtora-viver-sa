using AutoMapper;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Api.Mappers;

public class OrcamentoMappers : Profile
{
    public OrcamentoMappers()
    {
        CreateMap<OrcamentoRequest, OrcamentoDto>().ReverseMap();
    }
}