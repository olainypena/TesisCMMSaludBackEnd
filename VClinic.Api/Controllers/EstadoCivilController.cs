using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VClinic.Application.DTOs.EstadoCivil;
using VClinic.Application.Services;

namespace VClinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoCivilController : ControllerBase
    {
        private readonly EstadoCivilService _EstadoCivilService;

        public EstadoCivilController(EstadoCivilService EstadoCivilService)
        {
            _EstadoCivilService = EstadoCivilService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoCivilDetailDto>> GetById(int id, CancellationToken ct)
        {
            var centroEstadoCivil = await _EstadoCivilService.GetByIdAsync(id, ct);
            if (centroEstadoCivil is null)
            {
                return NotFound();
            }

            return Ok(centroEstadoCivil);
        }

        [HttpGet]
        public async Task<ActionResult<List<EstadoCivilDetailDto>>> GetByDate(CancellationToken ct)
        {
            var appointments = await _EstadoCivilService.GetByAllAsync(ct);
            return Ok(appointments);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id, CancellationToken ct)
        {
            await _EstadoCivilService.EliminarEstadoCivilAsync(id, ct);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] bool activarCentroEstadoCivil, CancellationToken ct)
        {
            var success = await _EstadoCivilService.UpdateStatusAsync(id, activarCentroEstadoCivil, ct);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] EstadoCivilInsDto request, CancellationToken ct)
        {
            var result = await _EstadoCivilService.CreateAsync(request, ct);
            return result;
        }

        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] EstadoCivilUpdDto request, CancellationToken ct)
        {
            await _EstadoCivilService.ActualizarEstadoCivilAsync(id, request, ct);
        }

    }
}
