
using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _log;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> log)
    {
        _next = next;
        _log  = log;
    }

    public async Task Invoke(HttpContext ctx)
    {
        try
        {
            await _next(ctx);
        }
        catch (FluentValidation.ValidationException vex)
{
    ctx.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    var errors = vex.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );
    await ctx.Response.WriteAsJsonAsync(new {
        title  = "Validation Failed",
        status = ctx.Response.StatusCode,
        errors
            });
        }
        catch (Exception ex)
        {
            // Cualquier otra excepción
            _log.LogError(ex, "Unhandled error");
            ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await ctx.Response.WriteAsJsonAsync(new {
                title   = "Internal Server Error",
                status  = ctx.Response.StatusCode,
                message = "Ha ocurrido un error inesperado."
            });
        }
    }
}
