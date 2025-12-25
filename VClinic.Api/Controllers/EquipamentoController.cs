using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VClinic.Application.DTOs.Equipamento;
using VClinic.Application.Services;

namespace VClinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipamentoController : ControllerBase
    {
        private readonly EquipamentoService _EquipamentoService;

        public EquipamentoController(EquipamentoService EquipamentoService)
        {
            _EquipamentoService = EquipamentoService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipamentoDetailDto>> GetById(int id, CancellationToken ct)
        {
            var centroEquipamento = await _EquipamentoService.GetByIdAsync(id, ct);
            if (centroEquipamento is null)
            {
                return NotFound();
            }

            return Ok(centroEquipamento);
        }

        [HttpGet]
        public async Task<ActionResult<List<EquipamentoDetailDto>>> GetByDate(CancellationToken ct)
        {
            var appointments = await _EquipamentoService.GetByAllAsync(ct);
            return Ok(appointments);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id, CancellationToken ct)
        {
            await _EquipamentoService.EliminarEquipamentoAsync(id, ct);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] bool activarCentroEquipamento, CancellationToken ct)
        {
            var success = await _EquipamentoService.UpdateStatusAsync(id, activarCentroEquipamento, ct);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] EquipamentoInsDto request, CancellationToken ct)
        {
            var result = await _EquipamentoService.CreateAsync(request, ct);
            return result;
        }

        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] EquipamentoUpdDto request, CancellationToken ct)
        {
            await _EquipamentoService.ActualizarEquipamentoAsync(id, request, ct);
        }

    }
}
