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
    public class ProfesionRepository : ICommunRepository<Profesion>
    {
        private readonly AppDbContext _context;

        public ProfesionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Profesion entity, CancellationToken cancellationToken = default)
        {
            await _context.Profesions.AddAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(Profesion entity, CancellationToken cancellationToken = default)
        {
            _context.Profesions.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Profesion entity, CancellationToken cancellationToken = default)
        {
            _context.Profesions.Update(entity);
            await Task.CompletedTask; // para respetar la firma async
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Profesion>> GetByAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Profesions
                 .AsNoTracking()
                 .ToListAsync(cancellationToken);
        }

        public async Task<Profesion?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Profesions
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.IdProfesion == id, cancellationToken);
        }

        public async Task<bool> HasRelatedDataAsync(int id, CancellationToken cancellationToken = default)
        {
            return true;
        }

        public async Task<bool> ExistsDataAsync(int id, string name, CancellationToken cancellationToken = default)
        {
            var existeNombre = await _context.Profesions
               .AnyAsync(s => s.IdProfesion != id && s.Nombre.ToUpper() == name.ToUpper(), cancellationToken);

            return existeNombre;
        }
    }
}
