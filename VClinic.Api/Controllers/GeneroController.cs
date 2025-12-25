using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VClinic.Application.DTOs.Genero;
using VClinic.Application.Services;

namespace VClinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly GeneroService _GeneroService;

        public GeneroController(GeneroService GeneroService)
        {
            _GeneroService = GeneroService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneroDetailDto>> GetById(int id, CancellationToken ct)
        {
            var centroGenero = await _GeneroService.GetByIdAsync(id, ct);
            if (centroGenero is null)
            {
                return NotFound();
            }

            return Ok(centroGenero);
        }

        [HttpGet]
        public async Task<ActionResult<List<GeneroDetailDto>>> GetByDate(CancellationToken ct)
        {
            var appointments = await _GeneroService.GetByAllAsync(ct);
            return Ok(appointments);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id, CancellationToken ct)
        {
            await _GeneroService.EliminarGeneroAsync(id, ct);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] bool activarCentroGenero, CancellationToken ct)
        {
            var success = await _GeneroService.UpdateStatusAsync(id, activarCentroGenero, ct);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] GeneroInsDto request, CancellationToken ct)
        {
            var result = await _GeneroService.CreateAsync(request, ct);
            return result;
        }

        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] GeneroUpdDto request, CancellationToken ct)
        {
            await _GeneroService.ActualizarGeneroAsync(id, request, ct);
        }

    }
}
