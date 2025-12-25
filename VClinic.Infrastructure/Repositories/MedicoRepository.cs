// VClinic.Infrastructure/Persistence/Repositories/MedicoRepository.cs
using Microsoft.EntityFrameworkCore;
using VClinic.Application.Abstractions;
using VClinic.Domain.Entities;
using VClinic.Infrastructure.Persistence;

namespace VClinic.Infrastructure.Repositories;

public sealed class MedicoRepository : IMedicoRepository
{
    private readonly AppDbContext _context;

    public MedicoRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> ExistsByPersonaIdAsync(long idPersona, CancellationToken ct = default)
    {
        return _context.Medicos.AnyAsync(m => m.IdPersona == idPersona, ct);
    }

    public Task<Medico?> GetByIdWithPersonaAsync(long idMedico, CancellationToken ct = default)
    {
        return _context.Medicos
            .Include(m => m.Persona)
            .FirstOrDefaultAsync(m => m.IdMedico == idMedico, ct);
    }

    public Task<List<Medico>> GetAllWithPersonaAsync(CancellationToken ct = default)
    {
        return _context.Medicos
            .AsNoTracking()
            .Include(m => m.Persona)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Medico medico, CancellationToken ct = default)
    {
        await _context.Medicos.AddAsync(medico, ct);
    }

    public Task UpdateAsync(Medico medico, CancellationToken ct = default)
    {
        _context.Medicos.Update(medico);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Medico medico, CancellationToken ct = default)
    {
        _context.Medicos.Remove(medico);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
    {
        return _context.SaveChangesAsync(ct);
    }

    public async Task<bool> HasRelatedDataAsync(long idMedico, CancellationToken cancellationToken = default)
    {
        return true;
    }

    public async Task<bool> ExistsDataAsync(long id, string NumeroColegiado, CancellationToken cancellationToken = default)
    {
        return true;
    }

    public Task<bool> ExistsByNroColegiadoAsync(long idmedico, string nrocolegiado, CancellationToken ct = default)
    {
        return _context.Medicos.AnyAsync(m => m.IdMedico != idmedico && m.NumeroColegiado.ToUpper().Trim() == nrocolegiado, ct);
    }
}
