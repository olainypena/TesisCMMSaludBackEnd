namespace VClinic.Api.Models
{
    public class ErrorResponse
    {
        public bool Success { get; set; } = false;              // Siempre false en errores
        public int StatusCode { get; set; }                     // 400, 404, 409, 500, etc.
        public string ErrorCode { get; set; } = default!;       // "ValidationError", "NotFound", "Conflict", "ServerError", etc.
        public string Message { get; set; } = default!;         // Mensaje amigable
        public IDictionary<string, string[]>? Errors { get; set; } // Errores de campo (solo para validación)
        public string? TraceId { get; set; }                    // Opcional, para debugging
    }
}
