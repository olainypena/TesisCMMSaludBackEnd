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
    public class OcupacionRepository: ICommunRepository<Ocupacion>
    {
        private readonly AppDbContext _context;

        public OcupacionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Ocupacion entity, CancellationToken cancellationToken = default)
        {
            await _context.Ocupacions.AddAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(Ocupacion entity, CancellationToken cancellationToken = default)
        {
            _context.Ocupacions.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Ocupacion entity, CancellationToken cancellationToken = default)
        {
            _context.Ocupacions.Update(entity);
            await Task.CompletedTask; // para respetar la firma async
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Ocupacion>> GetByAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Ocupacions
                 .AsNoTracking()
                 .ToListAsync(cancellationToken);
        }

        public async Task<Ocupacion?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Ocupacions
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.IdOcupacion == id, cancellationToken);
        }

        public async Task<bool> HasRelatedDataAsync(int id, CancellationToken cancellationToken = default)
        {
            return true;
        }

        public async Task<bool> ExistsDataAsync(int id, string name, CancellationToken cancellationToken = default)
        {
            var existeNombre = await _context.Ocupacions
               .AnyAsync(s => s.IdOcupacion != id && s.Nombre.ToUpper() == name.ToUpper(), cancellationToken);

            return existeNombre;
        }
    }
}
