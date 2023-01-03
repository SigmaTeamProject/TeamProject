using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Application.Exceptions;

namespace WebApi.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }
    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;
        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException);
                break;
            case NotFoundException notFoundException:
                code = HttpStatusCode.NotFound;
                break;
            case UnauthorizedAccessException unauthorizedAccessException:
                code = HttpStatusCode.Forbidden;
                break;
            case ArgumentException argumentException:
                code = HttpStatusCode.BadRequest;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(new {error = exception.Message});
        }

        return context.Response.WriteAsync(result);
    }
}