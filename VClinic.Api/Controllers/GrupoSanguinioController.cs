using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VClinic.Application.DTOs.GrupoSanguinio;
using VClinic.Application.Services;

namespace VClinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoSanguinioController : ControllerBase
    {
        private readonly GrupoSanguinioService _GrupoSanguinioService;

        public GrupoSanguinioController(GrupoSanguinioService GrupoSanguinioService)
        {
            _GrupoSanguinioService = GrupoSanguinioService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GrupoSanguinioDetailDto>> GetById(int id, CancellationToken ct)
        {
            var centroGrupoSanguinio = await _GrupoSanguinioService.GetByIdAsync(id, ct);
            if (centroGrupoSanguinio is null)
            {
                return NotFound();
            }

            return Ok(centroGrupoSanguinio);
        }

        [HttpGet]
        public async Task<ActionResult<List<GrupoSanguinioDetailDto>>> GetByDate(CancellationToken ct)
        {
            var appointments = await _GrupoSanguinioService.GetByAllAsync(ct);
            return Ok(appointments);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id, CancellationToken ct)
        {
            await _GrupoSanguinioService.EliminarGrupoSanguinioAsync(id, ct);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] bool activarCentroGrupoSanguinio, CancellationToken ct)
        {
            var success = await _GrupoSanguinioService.UpdateStatusAsync(id, activarCentroGrupoSanguinio, ct);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] GrupoSanguinioInsDto request, CancellationToken ct)
        {
            var result = await _GrupoSanguinioService.CreateAsync(request, ct);
            return result;
        }

        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] GrupoSanguinioUpdDto request, CancellationToken ct)
        {
            await _GrupoSanguinioService.ActualizarGrupoSanguinioAsync(id, request, ct);
        }

    }
}
