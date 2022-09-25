namespace ConstrutoraViverSA.Api.Controllers.Requests
{
    public abstract class BaseRequest<Dto>
    {
        public abstract Dto RequestParaDto();

        public abstract void Validar();

        public abstract void ValidarCriacao();

        public abstract void ValidarEdicao();
    }
}