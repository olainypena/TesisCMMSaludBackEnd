// VClinic.Application/Services/IPersonaService.cs
using VClinic.Application.DTOs.Persona;
using VClinic.Domain.Entities;

namespace VClinic.Application.Services;

public interface IPersonaService
{
    Task<DatosPersona> GetOrCreatePersonaAsync(PersonaInsDto data, CancellationToken ct = default);
    Task UpdatePersonaAsync(DatosPersona persona, PersonaDto data, CancellationToken ct = default);
    Task DeletePersonaAsync(long idpersona, CancellationToken ct = default);
}
