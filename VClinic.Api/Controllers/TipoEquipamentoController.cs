using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VClinic.Application.DTOs.TipoEquipamento;
using VClinic.Application.Services;

namespace VClinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEquipamentoController : ControllerBase
    {
        private readonly TipoEquipamentoService _TipoEquipamentoService;

        public TipoEquipamentoController(TipoEquipamentoService TipoEquipamentoService)
        {
            _TipoEquipamentoService = TipoEquipamentoService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoEquipamentoDetailDto>> GetById(int id, CancellationToken ct)
        {
            var centroTipoEquipamento = await _TipoEquipamentoService.GetByIdAsync(id, ct);
            if (centroTipoEquipamento is null)
            {
                return NotFound();
            }

            return Ok(centroTipoEquipamento);
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoEquipamentoDetailDto>>> GetByDate(CancellationToken ct)
        {
            var appointments = await _TipoEquipamentoService.GetByAllAsync(ct);
            return Ok(appointments);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id, CancellationToken ct)
        {
            await _TipoEquipamentoService.EliminarTipoEquipamentoAsync(id, ct);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] bool activarCentroTipoEquipamento, CancellationToken ct)
        {
            var success = await _TipoEquipamentoService.UpdateStatusAsync(id, activarCentroTipoEquipamento, ct);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] TipoEquipamentoInsDto request, CancellationToken ct)
        {
            var result = await _TipoEquipamentoService.CreateAsync(request, ct);
            return result;
        }

        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] TipoEquipamentoUpdDto request, CancellationToken ct)
        {
            await _TipoEquipamentoService.ActualizarTipoEquipamentoAsync(id, request, ct);
        }

    }
}
