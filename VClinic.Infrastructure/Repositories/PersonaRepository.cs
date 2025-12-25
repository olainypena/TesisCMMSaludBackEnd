// VClinic.Infrastructure/Persistence/Repositories/PersonaRepository.cs
using Microsoft.EntityFrameworkCore;
using VClinic.Application.Abstractions;
using VClinic.Domain.Entities;
using VClinic.Infrastructure.Persistence;
using VClinic.Infrastructure.Repositories;

namespace VClinic.Infrastructure.Repositories;

public sealed class PersonaRepository : IPersonaRepository
{
    private readonly AppDbContext _context;

    public PersonaRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<DatosPersona?> GetByIdentificacionAsync(int tipoIdentificacion, string identificacion, CancellationToken ct = default)
    {
        return _context.Personas
            .FirstOrDefaultAsync(p =>
                (int)p.IdTipoIdentificacion == tipoIdentificacion &&
                p.NumeroIdentificacion == identificacion, ct);
    }

    public Task<DatosPersona?> GetByIdAsync(long idPersona, CancellationToken ct = default)
    {
        return _context.Personas.FirstOrDefaultAsync(p => p.IdPersona == idPersona, ct);
    }

    public async Task AddAsync(DatosPersona persona, CancellationToken ct = default)
    {
        await _context.Personas.AddAsync(persona, ct);
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
    {
        return _context.SaveChangesAsync(ct);
    }   

    public Task UpdateAsync(DatosPersona persona, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(DatosPersona persona, CancellationToken ct = default)
    {
        _context.Personas.Remove(persona);
        return Task.CompletedTask;
    }

    public async Task<bool> HasRelatedDataAsync(long IdPersona, CancellationToken cancellationToken = default)
    {
        // Aquí revisas TODAS las tablas que dependan de la persona.

        var tieneMedicos = await _context.Medicos
            .AnyAsync(s => s.IdPersona == IdPersona, cancellationToken);

        if(tieneMedicos)
            return true;
        
        var tieneEmpleados = await _context.Empleados
            .AnyAsync(e => e.IdPersona == IdPersona, cancellationToken);

        if (tieneEmpleados)
            return true;

        var tienePacientes = await _context.Pacientes
            .AnyAsync(p => p.IdPersona == IdPersona, cancellationToken);

        if (tienePacientes)
            return true;
        

        return false;
    }

    public Task<bool> ExistsDataAsync(long id, int idTipoIdentificacion, string identificacion, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
