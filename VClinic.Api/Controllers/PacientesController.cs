using Microsoft.AspNetCore.Mvc;
using VClinic.Application.DTOs.Paciente;
using VClinic.Application.Services;

namespace VClinic.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : ControllerBase
    {
        private readonly PacienteService _pacienteService;

        public PacientesController(PacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PacienteLstDto>>> GetAll(CancellationToken ct)
        {
            var result = await _pacienteService.GetAllAsync(ct);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<PacienteDetailDto>> GetById(long id, CancellationToken ct)
        {
            var result = await _pacienteService.GetByIdAsync(id, ct);
            if (result is null)
                return NotFound(); // middleware lo transforma a ErrorResponse

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] PacienteInsDto request, CancellationToken ct)
        {
            var id = await _pacienteService.CrearPacienteAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Actualizar(long id, [FromBody] PacienteUpdDto request, CancellationToken ct)
        {
            await _pacienteService.ActualizarPacienteAsync(id, request, ct);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Eliminar(long id, CancellationToken ct)
        {
            await _pacienteService.EliminarPacienteAsync(id, ct);
            return NoContent();
        }
    }
}