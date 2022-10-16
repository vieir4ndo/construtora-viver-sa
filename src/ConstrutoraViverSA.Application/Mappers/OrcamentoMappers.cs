using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Application.Mappers;

[ExcludeFromCodeCoverage]
public class OrcamentoMappers : Profile
{
    public OrcamentoMappers()
    {
        CreateMap<Orcamento, OrcamentoDto>().ReverseMap();
    }
}