using InterUniversity.Application.Exceptions;
using InterUniversity.Domain.Dtos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace InterUniversity.Api.Middlewares;

public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task InvokeAsync(HttpContext context)
    {
        using var scope = logger.BeginScope(new Dictionary<string, object>
        {
            ["Path"] = context.Request.Path.Value!,
            ["Method"] = context.Request.Method,
            ["UserId"] = context.User.Identity?.Name ?? "Anonymous",
            ["RemoteIp"] = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown"
        });

        try
        {
            context.Request.EnableBuffering();

            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, message, typeException, isWarning) = MapException(exception);

        var customError = new CustomErrorResponse
        {
            StatusCode = statusCode,
            TypeException = typeException,
            Message = message,
            IsWarning = isWarning
        };

        if (isWarning)
        {
            logger.LogWarning("Validación de negocio fallida: {Message}", message);
        }
        else
        {
            string bodyPreview = await GetRequestBodyAsync(context.Request);
            logger.LogError(exception, "Error en la aplicación. Mensaje: {Message}. Body: {Body}", message, bodyPreview);
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsync(JsonSerializer.Serialize(customError, JsonOptions));
    }

    private static (int StatusCode, string Message, string TypeException, bool IsWarning) MapException(Exception exception)
    {
        return exception switch
        {
            // SQL Constraint (Foreign Key / Check)
            DbUpdateException { InnerException: SqlException { Number: 547 } }
                => ((int)HttpStatusCode.InternalServerError, "Conflicto con una restricción de la entidad. Transacción cancelada.", TypeException.Error, false),

            // Validaciones de negocio
            ValidationException valEx
                => ((int)HttpStatusCode.BadRequest, valEx.Message, TypeException.Warning, true),

            // Recurso no encontrado
            NotFoundException notFoundEx
                => ((int)HttpStatusCode.NotFound, notFoundEx.Message, TypeException.Error, false),

            // Excepciones base personalizadas
            BaseException baseEx => (baseEx.Code, baseEx.Message, baseEx.IsWarning ? TypeException.Warning : TypeException.Error, baseEx.IsWarning),

            // Error genérico (500)
            _ => ((int)HttpStatusCode.InternalServerError, "Intente de nuevo, si el problema persiste contacte con soporte.", TypeException.Error, false)
        };
    }

    private static async Task<string> GetRequestBodyAsync(HttpRequest request)
    {
        try
        {
            request.Body.Position = 0;
            using var reader = new StreamReader(request.Body, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0; // Resetear por si acaso

            return body.Length > 1000 ? body[..1000] + "..." : body;
        }
        catch
        {
            return "(No se pudo leer el body)";
        }
    }
}