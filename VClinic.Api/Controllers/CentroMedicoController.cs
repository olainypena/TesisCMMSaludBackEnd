using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VClinic.Application.DTOs.CentroMedico;
using VClinic.Application.Services;

namespace VClinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CentroMedicoController : ControllerBase
    {
        private readonly CentroMedicoService _centroMedicoService;

        public CentroMedicoController(CentroMedicoService centroMedicoService)
        {
            _centroMedicoService = centroMedicoService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CentroMedicoDto>> GetById(int id, CancellationToken ct)
        {
            var centroMedico = await _centroMedicoService.GetByIdAsync(id, ct);
            if (centroMedico is null)
            {
                return NotFound();
            }

            return Ok(centroMedico);
        }

        [HttpGet]
        public async Task<ActionResult<List<CentroMedicoDto>>> GetByDate(CancellationToken ct)
        {
            var appointments = await _centroMedicoService.GetByAllAsync(ct);
            return Ok(appointments);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id, CancellationToken ct)
        {
            await _centroMedicoService.EliminarCentroMedicoAsync(id, ct);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] bool activarCentroMedico, CancellationToken ct)
        {
            var success = await _centroMedicoService.UpdateStatusAsync(id, activarCentroMedico, ct);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CentroMedicoDto>> Create([FromBody] CentroMedicoInsDto request, CancellationToken ct)
        {
            var result = await _centroMedicoService.CreateAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdCentroMedico }, result);
        }

        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] CentroMedicoDto request, CancellationToken ct)
        {
            await _centroMedicoService.ActualizarCentroMedicoAsync(id, request, ct);
        }

    }
}
