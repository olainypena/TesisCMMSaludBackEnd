
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VClinic.Api.Middlewares;
using VClinic.Api.Models;
using VClinic.Application.Services;
using VClinic.Infrastructure.Persistence;
using VClinic.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});
//endregion

// Application / Infrastructure
builder.Services.AddApplicationServices();
//endregion

// Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//endregion

//CORS (React)
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
//endregion

// Validation response
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value?.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value!.Errors.Select(err => err.ErrorMessage).ToArray()
            );

        var errorResponse = new ErrorResponse
        {
            Success = false,
            StatusCode = StatusCodes.Status400BadRequest,
            ErrorCode = "ValidationError",
            Message = "Se encontraron errores de validación.",
            Errors = errors,
            TraceId = context.HttpContext.TraceIdentifier
        };

        return new BadRequestObjectResult(errorResponse);
    };
});
//endregion

var app = builder.Build();

// Apply migrations automatically
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}
//endregion

// Middlewares
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "VClinic API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;

    if (response.StatusCode >= 400 && response.StatusCode < 500 && !response.HasStarted)
    {
        response.ContentType = "application/json";

        var error = new ErrorResponse
        {
            Success = false,
            StatusCode = response.StatusCode,
            ErrorCode = response.StatusCode switch
            {
                StatusCodes.Status401Unauthorized => "Unauthorized",
                StatusCodes.Status403Forbidden => "Forbidden",
                StatusCodes.Status404NotFound => "NotFound",
                _ => "ClientError"
            },
            Message = response.StatusCode switch
            {
                StatusCodes.Status401Unauthorized => "No está autorizado para realizar esta acción.",
                StatusCodes.Status403Forbidden => "No tiene permisos para acceder a este recurso.",
                StatusCodes.Status404NotFound => "Recurso no encontrado.",
                _ => "Se ha producido un error en la solicitud."
            },
            TraceId = context.HttpContext.TraceIdentifier
        };

        await response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(error));
    }
});
//endregion

//HTTP pipeline
app.UseHttpsRedirection();

app.UseCors("ReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
//endregion
