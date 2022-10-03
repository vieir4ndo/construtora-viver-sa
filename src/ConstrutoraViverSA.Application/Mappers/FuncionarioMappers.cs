using AutoMapper;
using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Application.Mappers;

public class FuncionarioMappers: Profile
{
    public FuncionarioMappers()
    {
        CreateMap<Funcionario, FuncionarioDto>().ReverseMap();
    }
}