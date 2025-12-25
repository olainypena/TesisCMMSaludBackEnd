using System.Net;
using System.Text.Json;
using VClinic.Api.Models;                // ErrorResponse
using VClinic.Application.Common.Exceptions;

namespace VClinic.Api.Middlewares;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (AppException ex)
        {
            // Errores de negocio (4xx controlados)
            _logger.LogWarning(ex, "Error de negocio");

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = ex.StatusCode;

            var error = new ErrorResponse
            {
                Success = false,
                StatusCode = ex.StatusCode,
                ErrorCode = ex switch
                {
                    BadRequestException => "BadRequest",
                    NotFoundException => "NotFound",
                    ConflictException => "Conflict",
                    _ => "BusinessError"
                },
                Message = ex.Message,
                Errors = null,
                TraceId = context.TraceIdentifier
            };

            await response.WriteAsync(JsonSerializer.Serialize(error));
        }
        catch (Exception ex)
        {
            // Cualquier otra excepción → 500 estándar
            _logger.LogError(ex, "Error no controlado");

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var error = new ErrorResponse
            {
                Success = false,
                StatusCode = response.StatusCode,
                ErrorCode = "ServerError",
                Message = "Ha ocurrido un error inesperado. Contacte al administrador.",
                Errors = null,
                TraceId = context.TraceIdentifier
            };

            await response.WriteAsync(JsonSerializer.Serialize(error));
        }
    }
}
