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
    public class PosicionRepository : ICommunRepository<Posiciones>
    {
        private readonly AppDbContext _context;

        public PosicionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Posiciones entity, CancellationToken cancellationToken = default)
        {
            await _context.Posiciones.AddAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(Posiciones entity, CancellationToken cancellationToken = default)
        {
            _context.Posiciones.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Posiciones entity, CancellationToken cancellationToken = default)
        {
            _context.Posiciones.Update(entity);
            await Task.CompletedTask; // para respetar la firma async
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Posiciones>> GetByAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Posiciones
                 .AsNoTracking()
                 .ToListAsync(cancellationToken);
        }

        public async Task<Posiciones?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Posiciones
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.IdPosicion == id, cancellationToken);
        }

        public async Task<bool> HasRelatedDataAsync(int id, CancellationToken cancellationToken = default)
        {
            return true;
        }

        public async Task<bool> ExistsDataAsync(int id, string name, CancellationToken cancellationToken = default)
        {
            var existeNombre = await _context.Posiciones
               .AnyAsync(s => s.IdPosicion != id && s.Nombre.ToUpper() == name.ToUpper(), cancellationToken);

            return existeNombre;
        }
    }
}
