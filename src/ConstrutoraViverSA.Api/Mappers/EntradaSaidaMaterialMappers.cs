using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using ConstrutoraViverSA.Api.Controllers.Requests;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Api.Mappers;

[ExcludeFromCodeCoverage]
public class EntradaSaidaMaterialMappers : Profile
{
    public EntradaSaidaMaterialMappers()
    {
        CreateMap<EntradaSaidaMaterialRequest, EntradaSaidaMaterialDto>().ReverseMap();
    }
}