using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Domain.Mappers;

public class FuncionarioMappers: AutoMapper.Profile
{
    public FuncionarioMappers()
    {
        CreateMap<Funcionario, FuncionarioDto>().ReverseMap();
    }
}