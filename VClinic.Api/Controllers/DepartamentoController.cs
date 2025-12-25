using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VClinic.Application.DTOs.Departamento;
using VClinic.Application.Services;

namespace VClinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly DepartamentoService _departamentoService;

        public DepartamentoController(DepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartamentoDetailDto>> GetById(int id, CancellationToken ct)
        {
            var centroDepartamento = await _departamentoService.GetByIdAsync(id, ct);
            if (centroDepartamento is null)
            {
                return NotFound();
            }

            return Ok(centroDepartamento);
        }

        [HttpGet]
        public async Task<ActionResult<List<DepartamentoDetailDto>>> GetByDate(CancellationToken ct)
        {
            var appointments = await _departamentoService.GetByAllAsync(ct);
            return Ok(appointments);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id, CancellationToken ct)
        {
            await _departamentoService.EliminarDepartamentoAsync(id, ct);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] bool activarCentroDepartamento, CancellationToken ct)
        {
            var success = await _departamentoService.UpdateStatusAsync(id, activarCentroDepartamento, ct);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] DepartamentoInsDto request, CancellationToken ct)
        {
            var result = await _departamentoService.CreateAsync(request, ct);
            return result;
        }

        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] DepartamentoUpdDto request, CancellationToken ct)
        {
            await _departamentoService.ActualizarDepartamentoAsync(id, request, ct);
        }

    }
}
