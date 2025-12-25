using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VClinic.Application.DTOs.Profesion;
using VClinic.Application.Services;

namespace VClinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesionController : ControllerBase
    {
        private readonly ProfesionService _ProfesionService;

        public ProfesionController(ProfesionService ProfesionService)
        {
            _ProfesionService = ProfesionService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfesionDetailDto>> GetById(int id, CancellationToken ct)
        {
            var centroProfesion = await _ProfesionService.GetByIdAsync(id, ct);
            if (centroProfesion is null)
            {
                return NotFound();
            }

            return Ok(centroProfesion);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProfesionDetailDto>>> GetByDate(CancellationToken ct)
        {
            var appointments = await _ProfesionService.GetByAllAsync(ct);
            return Ok(appointments);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id, CancellationToken ct)
        {
            await _ProfesionService.EliminarProfesionAsync(id, ct);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] bool activarCentroProfesion, CancellationToken ct)
        {
            var success = await _ProfesionService.UpdateStatusAsync(id, activarCentroProfesion, ct);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] ProfesionInsDto request, CancellationToken ct)
        {
            var result = await _ProfesionService.CreateAsync(request, ct);
            return result;
        }

        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] ProfesionUpdDto request, CancellationToken ct)
        {
            await _ProfesionService.ActualizarProfesionAsync(id, request, ct);
        }

    }
}
