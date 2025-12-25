using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Application.Abstractions;
using VClinic.Domain.Entities.Catalogos;
using VClinic.Infrastructure.Persistence;

namespace VClinic.Infrastructure.Repositories
{
    public class EstadoCivilRepository: IEstadoCivilRepository
    {
        private readonly AppDbContext _context;

        public EstadoCivilRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(EstadoCivil estadoCivil, CancellationToken cancellationToken = default)
        {
            await _context.EstadoCivils.AddAsync(estadoCivil, cancellationToken);
        }

        public async Task<List<EstadoCivil>> GetByAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.EstadoCivils
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<EstadoCivil?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.EstadoCivils
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.IdEstadoCivil == id, cancellationToken);
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(EstadoCivil estadoCivil, CancellationToken cancellationToken = default)
        {
            _context.EstadoCivils.Update(estadoCivil);
            await Task.CompletedTask; // para respetar la firma async
        }

        public async Task DeleteAsync(EstadoCivil estadoCivil, CancellationToken cancellationToken = default)
        {
            _context.EstadoCivils.Remove(estadoCivil);
            await Task.CompletedTask;
        }

        public async Task<bool> HasRelatedDataAsync(int estadoCivil, CancellationToken cancellationToken = default)
        {
            return true;
        }

        public async Task<bool> ExistsDataAsync(int id, string nombre, CancellationToken cancellationToken = default)
        {
            var existeNombre = await _context.EstadoCivils
               .AnyAsync(s => s.IdEstadoCivil != id && s.Nombre.ToUpper() == nombre.ToUpper(), cancellationToken);

            return existeNombre;
        }
    }
}
