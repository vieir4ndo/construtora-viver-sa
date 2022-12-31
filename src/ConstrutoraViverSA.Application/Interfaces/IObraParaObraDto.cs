using ConstrutoraViverSA.Domain;
using ConstrutoraViverSA.Domain.Dtos;

namespace ConstrutoraViverSA.Application.Interfaces;

public interface IObraParaObraDto
{
    ObraDto Mapear(Obra obra);
}