// VClinic.Infrastructure/Persistence/Repositories/EmpleadoRepository.cs
using Microsoft.EntityFrameworkCore;
using VClinic.Application.Abstractions;
using VClinic.Domain.Entities;

namespace VClinic.Infrastructure.Persistence.Repositories;

public sealed class EmpleadoRepository : IEmpleadoRepository
{
    private readonly AppDbContext _context;

    public EmpleadoRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> ExistsByPersonaIdAsync(long idPersona, CancellationToken ct = default)
    {
        return _context.Empleados.AnyAsync(e => e.IdPersona == idPersona, ct);
    }

    public Task<Empleado?> GetByIdWithPersonaAsync(long idEmpleado, CancellationToken ct = default)
    {
        return _context.Empleados
            .Include(e => e.Persona)
            .FirstOrDefaultAsync(e => e.IdEmpleado == idEmpleado, ct);
    }

    public Task<List<Empleado>> GetAllWithPersonaAsync(CancellationToken ct = default)
    {
        return _context.Empleados
            .AsNoTracking()
            .Include(e => e.Persona)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Empleado empleado, CancellationToken ct = default)
    {
        await _context.Empleados.AddAsync(empleado, ct);
    }

    public Task UpdateAsync(Empleado empleado, CancellationToken ct = default)
    {
        _context.Empleados.Update(empleado);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Empleado empleado, CancellationToken ct = default)
    {
        _context.Empleados.Remove(empleado);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
    {
        return _context.SaveChangesAsync(ct);
    }
}
