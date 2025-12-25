// VClinic.Infrastructure/Persistence/Repositories/PacienteRepository.cs
using Microsoft.EntityFrameworkCore;
using VClinic.Application.Abstractions;
using VClinic.Domain.Entities;

namespace VClinic.Infrastructure.Persistence.Repositories;

public sealed class PacienteRepository : IPacienteRepository
{
    private readonly AppDbContext _context;

    public PacienteRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> ExistsByPersonaIdAsync(long idPersona, CancellationToken ct = default)
    {
        return _context.Pacientes.AnyAsync(p => p.IdPersona == idPersona, ct);
    }

    public Task<Paciente?> GetByIdWithPersonaAsync(long idPaciente, CancellationToken ct = default)
    {
        return _context.Pacientes
            .Include(p => p.Persona)
            .FirstOrDefaultAsync(p => p.IdPaciente == idPaciente, ct);
    }

    public Task<List<Paciente>> GetAllWithPersonaAsync(CancellationToken ct = default)
    {
        return _context.Pacientes
            .AsNoTracking()
            .Include(p => p.Persona)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Paciente paciente, CancellationToken ct = default)
    {
        await _context.Pacientes.AddAsync(paciente, ct);
    }

    public Task UpdateAsync(Paciente paciente, CancellationToken ct = default)
    {
        _context.Pacientes.Update(paciente);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Paciente paciente, CancellationToken ct = default)
    {
        _context.Pacientes.Remove(paciente);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
    {
        return _context.SaveChangesAsync(ct);
    }
}
