using System.Diagnostics.CodeAnalysis;
using System.Net;
using ConstrutoraViverSA.Api.Controllers.Responses;
using ConstrutoraViverSA.Domain.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ConstrutoraViverSA.Api.Middlewares;

[ExcludeFromCodeCoverage]
public static class ErrorHandlerExtensions
{
    public static IApplicationBuilder UseErrorHandler(
        this IApplicationBuilder appBuilder)
    {
        return appBuilder.UseExceptionHandler(builder =>
        {
            builder.Run(async context =>
            {
                var exceptionHandlerFeature = context
                    .Features
                    .Get<IExceptionHandlerFeature>();
                var exceptionHandlerPathFeature =
                    context.Features.Get<IExceptionHandlerPathFeature>();

                if (exceptionHandlerFeature != null)
                {
                    if (exceptionHandlerPathFeature?.Error is NaoEncontradoException naoEncontradoException)
                    {
                        var response = new ResponseApi<object>(false, null, naoEncontradoException.Message);

                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                    }
                    else if (exceptionHandlerPathFeature?.Error is ErroValidacaoException erroValidacaoException)
                    {
                        var response = new ResponseApi<object>(false, null, erroValidacaoException.Message);

                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                    }
                    else if (exceptionHandlerPathFeature?.Error is OperacaoInvalidaException invalidOperation)
                    {
                        var response = new ResponseApi<object>(false, null, invalidOperation.Message);

                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                    }
                    else
                    {
                        var response = new ResponseApi<object>(false, null,
                            "Houve um problema ao realizar essa operação, por favor tente novamente mais tarde. Se os problemas persistirem, entre em contato com o suporte.");

                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                    }
                }
            });
        });
    }
}