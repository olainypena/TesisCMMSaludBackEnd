using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VClinic.Application.DTOs.Posicion;
using VClinic.Application.Services;

namespace VClinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PosicionController : ControllerBase
    {
        private readonly PosicionService _PosicionService;

        public PosicionController(PosicionService PosicionService)
        {
            _PosicionService = PosicionService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PosicionDetailDto>> GetById(int id, CancellationToken ct)
        {
            var centroPosicion = await _PosicionService.GetByIdAsync(id, ct);
            if (centroPosicion is null)
            {
                return NotFound();
            }

            return Ok(centroPosicion);
        }

        [HttpGet]
        public async Task<ActionResult<List<PosicionDetailDto>>> GetByDate(CancellationToken ct)
        {
            var appointments = await _PosicionService.GetByAllAsync(ct);
            return Ok(appointments);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id, CancellationToken ct)
        {
            await _PosicionService.EliminarPosicionAsync(id, ct);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] bool activarCentroPosicion, CancellationToken ct)
        {
            var success = await _PosicionService.UpdateStatusAsync(id, activarCentroPosicion, ct);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] PosicionInsDto request, CancellationToken ct)
        {
            var result = await _PosicionService.CreateAsync(request, ct);
            return result;
        }

        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] PosicionUpdDto request, CancellationToken ct)
        {
            await _PosicionService.ActualizarPosicionAsync(id, request, ct);
        }

    }
}
