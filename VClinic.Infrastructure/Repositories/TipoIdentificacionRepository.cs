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
    public class TipoIdentificacionRepository : ICommunRepository<TipoIdentificacion>
    {
        private readonly AppDbContext _context;

        public TipoIdentificacionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TipoIdentificacion entity, CancellationToken cancellationToken = default)
        {
            await _context.TipoIdentificacions.AddAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(TipoIdentificacion entity, CancellationToken cancellationToken = default)
        {
            _context.TipoIdentificacions.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(TipoIdentificacion entity, CancellationToken cancellationToken = default)
        {
            _context.TipoIdentificacions.Update(entity);
            await Task.CompletedTask; // para respetar la firma async
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<TipoIdentificacion>> GetByAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.TipoIdentificacions
                 .AsNoTracking()
                 .ToListAsync(cancellationToken);
        }

        public async Task<TipoIdentificacion?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.TipoIdentificacions
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.IdTipoIdentificacion == id, cancellationToken);
        }

        public async Task<bool> HasRelatedDataAsync(int id, CancellationToken cancellationToken = default)
        {
            return true;
        }

        public async Task<bool> ExistsDataAsync(int id, string name, CancellationToken cancellationToken = default)
        {
            var existeNombre = await _context.TipoIdentificacions
               .AnyAsync(s => s.IdTipoIdentificacion != id && s.Nombre.ToUpper() == name.ToUpper(), cancellationToken);

            return existeNombre;
        }
    }
}
