using AutoMapper;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Application.Mappers;

public class OrcamentoMappers : Profile
{
    public OrcamentoMappers()
    {
        CreateMap<Orcamento, OrcamentoDto>().ReverseMap();
    }
}