using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VClinic.Application.DTOs.TipoIdentificacion;
using VClinic.Application.Services;

namespace VClinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoIdentificacionController : ControllerBase
    {
        private readonly TipoIdentificacionService _TipoIdentificacionService;

        public TipoIdentificacionController(TipoIdentificacionService TipoIdentificacionService)
        {
            _TipoIdentificacionService = TipoIdentificacionService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoIdentificacionDetailDto>> GetById(int id, CancellationToken ct)
        {
            var centroTipoIdentificacion = await _TipoIdentificacionService.GetByIdAsync(id, ct);
            if (centroTipoIdentificacion is null)
            {
                return NotFound();
            }

            return Ok(centroTipoIdentificacion);
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoIdentificacionDetailDto>>> GetByDate(CancellationToken ct)
        {
            var appointments = await _TipoIdentificacionService.GetByAllAsync(ct);
            return Ok(appointments);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id, CancellationToken ct)
        {
            await _TipoIdentificacionService.EliminarTipoIdentificacionAsync(id, ct);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] bool activarCentroTipoIdentificacion, CancellationToken ct)
        {
            var success = await _TipoIdentificacionService.UpdateStatusAsync(id, activarCentroTipoIdentificacion, ct);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] TipoIdentificacionInsDto request, CancellationToken ct)
        {
            var result = await _TipoIdentificacionService.CreateAsync(request, ct);
            return result;
        }

        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] TipoIdentificacionUpdDto request, CancellationToken ct)
        {
            await _TipoIdentificacionService.ActualizarTipoIdentificacionAsync(id, request, ct);
        }

    }
}
