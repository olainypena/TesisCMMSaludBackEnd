using Microsoft.AspNetCore.Mvc;
using VClinic.Application.DTOs.Empleado;
using VClinic.Application.Services;

namespace VClinic.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadosController : ControllerBase
    {
        private readonly EmpleadoService _empleadoService;

        public EmpleadosController(EmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmpleadoLstDto>>> GetAll(CancellationToken ct)
        {
            var result = await _empleadoService.GetAllAsync(ct);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<EmpleadoDetailDto>> GetById(long id, CancellationToken ct)
        {
            var result = await _empleadoService.GetByIdAsync(id, ct);
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] EmpleadoInsDto request, CancellationToken ct)
        {
            var id = await _empleadoService.CrearEmpleadoAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Actualizar(long id, [FromBody] EmpleadoUpdDto request, CancellationToken ct)
        {
            await _empleadoService.ActualizarEmpleadoAsync(id, request, ct);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Eliminar(long id, CancellationToken ct)
        {
            await _empleadoService.EliminarEmpleadoAsync(id, ct);
            return NoContent();
        }
    }
}