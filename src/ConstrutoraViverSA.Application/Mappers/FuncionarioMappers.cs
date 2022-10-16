using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Application.Mappers;

[ExcludeFromCodeCoverage]
public class FuncionarioMappers: Profile
{
    public FuncionarioMappers()
    {
        CreateMap<Funcionario, FuncionarioDto>().ReverseMap();
    }
}