// VClinic.Api/Controllers/MedicosController.cs
using Microsoft.AspNetCore.Mvc;
using VClinic.Application.DTOs.Medico;
using VClinic.Application.Services;

namespace VClinic.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicosController : ControllerBase
{
    private readonly MedicoService _medicoService;

    public MedicosController(MedicoService medicoService)
    {
        _medicoService = medicoService;
    }

    [HttpGet]
    public async Task<ActionResult<List<MedicoLstDto>>> GetAll(CancellationToken ct)
    {
        var result = await _medicoService.GetAllAsync(ct);
        return Ok(result);
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<MedicoDetailDto>> GetById(long id, CancellationToken ct)
    {
        var result = await _medicoService.GetByIdAsync(id, ct);
        if (result is null)
            return NotFound(); // tu middleware/statuscodepages lo formatea a ErrorResponse

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] MedicoInsDto request, CancellationToken ct)
    {
        var id = await _medicoService.CrearMedicoAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Actualizar(long id, [FromBody] MedicoUpdDto request, CancellationToken ct)
    {
        await _medicoService.ActualizarMedicoAsync(id, request, ct);
        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Eliminar(long id, CancellationToken ct)
    {
        await _medicoService.EliminarMedicoAsync(id, ct);
        return NoContent();
    }
}
