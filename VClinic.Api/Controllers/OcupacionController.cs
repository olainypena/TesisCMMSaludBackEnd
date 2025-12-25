using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VClinic.Application.DTOs.Ocupacion;
using VClinic.Application.Services;

namespace VClinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OcupacionController : ControllerBase
    {
        private readonly OcupacionService _OcupacionService;

        public OcupacionController(OcupacionService OcupacionService)
        {
            _OcupacionService = OcupacionService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OcupacionDetailDto>> GetById(int id, CancellationToken ct)
        {
            var centroOcupacion = await _OcupacionService.GetByIdAsync(id, ct);
            if (centroOcupacion is null)
            {
                return NotFound();
            }

            return Ok(centroOcupacion);
        }

        [HttpGet]
        public async Task<ActionResult<List<OcupacionDetailDto>>> GetByDate(CancellationToken ct)
        {
            var appointments = await _OcupacionService.GetByAllAsync(ct);
            return Ok(appointments);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id, CancellationToken ct)
        {
            await _OcupacionService.EliminarOcupacionAsync(id, ct);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] bool activarCentroOcupacion, CancellationToken ct)
        {
            var success = await _OcupacionService.UpdateStatusAsync(id, activarCentroOcupacion, ct);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] OcupacionInsDto request, CancellationToken ct)
        {
            var result = await _OcupacionService.CreateAsync(request, ct);
            return result;
        }

        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] OcupacionUpdDto request, CancellationToken ct)
        {
            await _OcupacionService.ActualizarOcupacionAsync(id, request, ct);
        }

    }
}
